using Branch.Models;
using Branch.Models.NoSQL;
using Branch.SearchAuxiliars;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Branch.Controllers
{
    public class SentimentalAnalysis
    {
        public string sentence { get; set; }
        public string topic { get; set; }
        public string Topic_norm { get; set; }
        public string text { get; set; }
        public string text_norm { get; set; }
        public string score { get; set; }
    }

    public class PublicAPIController : ApiController
    {
        private readonly SQLContext SQLContext = new SQLContext();
        private readonly HttpClient ExternalApi = new HttpClient();

        private readonly string Token = "63096c5c035e43efb8fba206a1aec2d2";
        private readonly string URL = "https://svc02.api.bitext.com/sentiment/";

        [HttpGet]
        [Route("api/subject/related")]
        public async Task<IHttpActionResult> RelatedSubjects()
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/subject/opinion")]
        public async Task<IHttpActionResult> OpinionOnSubject([FromUri] int SubjectId)
        {
            var Subject = SQLContext.Subjects.Find(SubjectId);
            var SubjectPosts = PostAuxiliar.PostsBySubject(SubjectId);

            double Opinion = 0;

            foreach (var Post in SubjectPosts)
            {
                var Response = await MakeRequest(Post.Text.Replace("#", "")).ConfigureAwait(false);
                var Analysis = (Response as JArray).ToObject<List<SentimentalAnalysis>>();

                Opinion += CalculateMeanScore(new List<string>()
                {
                   Subject.Hashtag.Replace("#", "")
                }, Analysis);
            }

            return Ok(new { Topic = Subject.Hashtag, Score = Opinion, Opinion = ScoreTranslator(Opinion)});
        }

        [HttpGet]
        [Route("api/product/opinion")]
        public async Task<IHttpActionResult> OpinionOnProduct([FromUri] int ProductId)
        {
            var ProductPosts = PostAuxiliar.PostsByProduct(ProductId);
            var Product = SQLContext.Products.Find(ProductId);

            double Opinion = 0;

            foreach (var Post in ProductPosts)
            {
                var Response = await MakeRequest(Post.Text.Replace("$", "")).ConfigureAwait(false);
                var Analysis = (Response as JArray).ToObject<List<SentimentalAnalysis>>();

                Opinion += CalculateMeanScore(new List<string>()
                {
                    Product.Name,
                    Product.Description,
                    Product.Pro.Firstname,
                    Product.Pro.Lastname,
                    Product.Pro.Nickname
                }, Analysis);
            }

            return Ok(new { Product = Product.Name, Score = Opinion, Opinion = ScoreTranslator(Opinion) });
        }

        private string ScoreTranslator(double Score)
        {
            if (Score > 0)
            {
                return "Good";
            }
            else if (Score < 0)
            {
                return "Bad";
            }
            else
            {
                return "Neutral";
            }
        }

        private double CalculateMeanScore(List<string> Topics, List<SentimentalAnalysis> Analysis)
        {
            double Opinion = 0;

            Opinion += (Analysis).Where(x => ContainsAny(x.topic, Topics))
                                 .Sum(x => double.Parse(x.score, CultureInfo.InvariantCulture)) / Analysis.Count;

            return Opinion;
        }

        private bool ContainsAny(string Original, List<string> Searched)
        {
            foreach (var Str in Searched)
            {
                if (Original.Contains(Str))
                {
                    return true;
                }
            }

            return false;
        }

        private async Task<dynamic> MakeRequest(string Text)
        {
            ExternalApi.DefaultRequestHeaders.Add("Authorization", "bearer " + Token);

            var Response = await ExternalApi
                                            .PostAsJsonAsync(URL, new { language = "por", text = Text })
                                            .ConfigureAwait(false);

            if (!Response.IsSuccessStatusCode)
            {
                return null;
            }

            var Data = await Response.Content
                                             .ReadAsAsync<dynamic>()
                                             .ConfigureAwait(false);

            var ResultId = Data.resultid;

            var IsFirstTime = true;

            do
            {
                await Task.Run(async () =>
                  {
                      if (!IsFirstTime)
                      {
                          _ = Task.Delay(200);
                      }

                      Response = await ExternalApi
                                              .GetAsync(URL + ResultId + '/');

                      Data = await Response.Content
                                                      .ReadAsAsync<dynamic>()
                                                      .ConfigureAwait(true);

                  }).ConfigureAwait(false);

                IsFirstTime = false;
            } while (Data.sentimentanalysis == null);

            return Data.sentimentanalysis;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SQLContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

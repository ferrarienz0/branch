using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Branch.Controllers
{
    public class PublicAPIController : ApiController
    {
        private readonly HttpClient ExternalApi = new HttpClient();
        private readonly string Token = "<TOKEN>";
        private readonly string URL = "https://svc02.api.bitext.com/sentiment/";
        private readonly Dictionary<string, string> Language = new Dictionary<string, string>()
        {
            { "English" , "eng" },
            { "Portuguese", "por"},
            { "Spanish", "spa" }
        };

        [HttpGet]
        [Route("api/semantic")]
        public async Task<IHttpActionResult> TestAPI()
        {
            return Ok();
        }

        private async Task<dynamic> MakeRequest(string Text, string Language)
        {
            ExternalApi.DefaultRequestHeaders.Add("Authorization", "bearer " + Token);

            var Response = await ExternalApi
                                            .PostAsJsonAsync(URL, new { language = Language, text = Text })
                                            .ConfigureAwait(false);

            if(!Response.IsSuccessStatusCode)
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
                          _ = Task.Delay(1000);
                      }

                      Response = await ExternalApi
                                              .GetAsync(URL + ResultId + '/');

                      Data = await Response.Content
                                                      .ReadAsAsync<dynamic>()
                                                      .ConfigureAwait(true);

                  }).ConfigureAwait(false);

            } while (Data.sentimentanalysis == null);

            return Data.sentimentanalysis;
        }
    }
}

using Branch.JWTProvider;
using Branch.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Branch.Controllers
{
    public class MediaController : ApiController
    {
        private readonly SQLContext SQLContext = new SQLContext();
        private readonly string Root = HttpContext.Current.Server.MapPath("~/Media_Data");

        [HttpPost]
        [Route("media/create")]
        [ResponseType(typeof(List<Media>))]
        public async Task<IHttpActionResult> PostMedia([FromUri] string AccessToken, bool IsUserMedia)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Provider = new MultipartFormDataStreamProvider(Root);

            var Medias = new List<Media>();

            try
            {
                await Request.Content.ReadAsMultipartAsync(Provider).ConfigureAwait(false);

                foreach (var _File in Provider.FileData)
                {

                    var FileData = HandleFile(_File);
                    var Name = (string) FileData.Name;
                    var FileExtension = (string) FileData.FileExtension;

                    var NewMedia = TreatMediaCreation(Name, FileExtension, UserId, IsUserMedia);

                    Medias.Add(NewMedia);
                }

            }
            catch
            {
                return InternalServerError();
            }

            return Ok(Medias);
        }

        [HttpGet]
        [Route("media")]
        public IHttpActionResult MediaById([FromUri] int MediaId)
        {
            var Media = SQLContext.Medias.Find(MediaId);

            return Ok(Media);
        }

        private dynamic HandleFile(MultipartFileData _File)
        {
            var Name = Guid.NewGuid().ToString();
            var LocalFileName = _File.LocalFileName;

            var FileExtension = _File.Headers.ContentDisposition.FileName;
            var PointIndex = FileExtension.LastIndexOf('.');
            FileExtension = FileExtension.Substring(PointIndex, FileExtension.Length - PointIndex - 1);

            Name += FileExtension;

            var FilePath = Path.Combine(Root, Name);
            File.Move(LocalFileName, FilePath);

            dynamic FileData = new { Name, FileExtension };

            return FileData; 
        }

        private Media TreatMediaCreation(string Name, string FileExtension, int UserId, bool IsUserMedia)
        {
            var MediaType = SQLContext.TypeMedias.Where(x => x.Name == FileExtension).FirstOrDefault();

            if (MediaType == default)
            {
                MediaType = new TypeMedia { Name = FileExtension };
                
                SQLContext.TypeMedias.Add(MediaType);
                SQLContext.SaveChanges();
            }

            var NewMedia = new Media { URL = Url.Content(Path.Combine("~/Media_Data", Name)) };

            SQLContext.Medias.Add(NewMedia);
            SQLContext.SaveChanges();

            if(IsUserMedia)
            {
                var User = SQLContext.Users.Find(UserId);
                User.MediaId = NewMedia.Id;
                SQLContext.Entry(User).State = System.Data.Entity.EntityState.Modified;
            }

            SQLContext.SaveChanges();

            return NewMedia;
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

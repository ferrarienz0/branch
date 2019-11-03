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
        private readonly Context DB = new Context();
        private readonly string Root = HttpContext.Current.Server.MapPath("~/Media_Data");

        [HttpPost]
        [Route("media")]
        [ResponseType(typeof(List<Media>))]
        public async Task<IHttpActionResult> PostMedia([FromUri] string AccessToken, bool IsUserMedia)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Provider = new MultipartFormDataStreamProvider(Root);

            var Medias = new List<Media>();

            try
            {
                await Request.Content.ReadAsMultipartAsync(Provider);

                foreach (var _File in Provider.FileData)
                {

                    var FileData = HandleFile(_File);
                    var Name = (string) FileData.Name;
                    var FileExtension = (string) FileData.FileExtension;

                    var NewMedia = await TreatMediaCreation(Name, FileExtension, UserId, IsUserMedia);

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
        public IHttpActionResult GetMedia([FromUri] int id)
        {
            var Media = DB.Medias.Find(id);

            return Ok(Media);
        }

        private dynamic HandleFile(MultipartFileData _File)
        {
            var Name = Guid.NewGuid().ToString();
            var LocalFileName = _File.LocalFileName;

            var FileExtension = _File.Headers.ContentDisposition.FileName;
            var PointIndex = FileExtension.IndexOf('.');
            FileExtension = FileExtension.Substring(PointIndex, FileExtension.Length - PointIndex - 1);

            Name += FileExtension;

            var FilePath = Path.Combine(Root, Name);
            File.Move(LocalFileName, FilePath);

            dynamic FileData = new { Name, FileExtension };

            return FileData; 
        }

        private async Task<Media> TreatMediaCreation(string Name, string FileExtension, int UserId, bool IsUserMedia)
        {
            var MediaType = DB.TypeMedias.Where(x => x.Name == FileExtension).FirstOrDefault();

            if (MediaType == default)
            {
                MediaType = new TypeMedia { Name = FileExtension };
                DB.TypeMedias.Add(MediaType);
                await DB.SaveChangesAsync();
            }

            var NewMedia = new Media { URL = Url.Content(Path.Combine("~/Media_Data", Name)) };

            DB.Medias.Add(NewMedia);
            await DB.SaveChangesAsync();

            if(IsUserMedia)
            {
                var User = await DB.Users.FindAsync(UserId);
                User.MediaId = NewMedia.Id;
                DB.Entry(User).State = System.Data.Entity.EntityState.Modified;
            }

            await DB.SaveChangesAsync();

            return NewMedia;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

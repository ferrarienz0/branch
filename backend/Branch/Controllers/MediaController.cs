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
        private readonly string Root = HttpContext.Current.Server.MapPath("~/App_Data");

        [HttpPost]
        [Route("media")]
        [ResponseType(typeof(List<Media>))]
        public async Task<IHttpActionResult> PostMedia([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Provider = new MultipartFormDataStreamProvider(Root);

            var Medias = new List<Media>();

            try
            {
                await Request.Content.ReadAsMultipartAsync(Provider);

                foreach (var _File in Provider.FileData)
                {

                    var FileData = HandleFile(_File, Root);
                    var Name = (string) FileData.Name;
                    var FileExtension = (string) FileData.FileExtension;

                    var NewMedia = await TreatMediaCreation(Root, Name, FileExtension, UserId);

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
        public HttpResponseMessage GetMedia([FromUri] int Id)
        {
            var Media = DB.Medias.Find(Id);

            var Result = new HttpResponseMessage(HttpStatusCode.OK);

            var FileStream = new FileStream(Path.Combine(Root, Media.URL), FileMode.Open);
            var _Image = Image.FromStream(FileStream);
            var MemoryStream = new MemoryStream();

            _Image.Save(MemoryStream, ImageFormat.Png);

            Result.Content = new ByteArrayContent(MemoryStream.ToArray());
            Result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            return Result;
        }

        private dynamic HandleFile(MultipartFileData _File, string Root)
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

        private async Task<Media> TreatMediaCreation(string Root, string Name, string FileExtension, int UserId)
        {
            var MediaType = DB.TypeMedias.Where(x => x.Name == FileExtension).FirstOrDefault();

            if (MediaType == default)
            {
                MediaType = new TypeMedia { Name = FileExtension };
                DB.TypeMedias.Add(MediaType);
                await DB.SaveChangesAsync();
            }

            var NewMedia = new Media { URL = Name };

            DB.Medias.Add(NewMedia);
            await DB.SaveChangesAsync();

            DB.UserMedias.Add(new UserMedia { MediaId = NewMedia.Id, UserId = UserId, TypeMediaId = MediaType.Id });

            var NewMediaCopy = new Media
            {
                Id = NewMedia.Id,
                URL = Path.Combine(Root, NewMedia.URL),
                CreatedAt = NewMedia.CreatedAt,
                UpdatedAt = NewMedia.UpdatedAt
            };

            return NewMediaCopy;
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

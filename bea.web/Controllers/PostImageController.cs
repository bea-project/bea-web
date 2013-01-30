using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bea.Core.Services;

namespace Bea.Web.Controllers
{
    public class PostImageController : Controller
    {
        private readonly IAdImageServices _adImageServices;

        public PostImageController(IAdImageServices adImageServices)
        {
            if (adImageServices == null)
                throw new ArgumentNullException("adImageServices");

            _adImageServices = adImageServices;
        }

        //
        // GET: /PostImage/Get/{id}
        [AcceptVerbs(HttpVerbs.Get)]
        //[OutputCache(CacheProfile = "AdImages")]
        public ActionResult Get(String id)
        {
            byte[] imageBytes = _adImageServices.GetAdImage(id, false);

            if (imageBytes == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return new FileContentResult(imageBytes, "image/jpg");
        }

        //
        // POST: /PostImage/UploadImage
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadImage()
        {
            IList<String> imagesGuid = new List<string>();
            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file];
                if (hpf.ContentLength == 0)
                    continue;

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(hpf.InputStream))
                {
                    fileData = binaryReader.ReadBytes(hpf.ContentLength);
                }

                Guid result = _adImageServices.StoreImage(hpf.FileName, fileData);
                imagesGuid.Add(result.ToString());
            }

            return Json(imagesGuid);
        }

    }
}

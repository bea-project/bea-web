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
    public class AdImageController : Controller
    {
        private readonly IAdImageServices _adImageServices;

        public AdImageController(IAdImageServices adImageServices)
        {
            if (adImageServices == null)
                throw new ArgumentNullException("adImageServices");

            _adImageServices = adImageServices;
        }

        //
        // GET: /AdImage/Get/{id}
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
        // POST: /AdImage/UploadImage
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

                string savedFileName = Path.Combine(
                   Server.MapPath("~/App_Data/Images"),
                   Path.GetFileName(hpf.FileName));

                hpf.SaveAs(savedFileName);
                
                Guid result = _adImageServices.StoreImage(hpf.FileName, System.IO.File.ReadAllBytes(savedFileName));
                imagesGuid.Add(result.ToString());
                System.IO.File.Delete(savedFileName);
            }

            return Json(imagesGuid);
        }

    }
}

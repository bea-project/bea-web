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
        // GET: /AdImage/
        [AcceptVerbs(HttpVerbs.Get)]
        //[OutputCache(CacheProfile = "AdImages")]
        public ActionResult Index(String imageId)
        {
            byte[] imageBytes = _adImageServices.GetAdImage(imageId, false);

            if (imageBytes == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return new FileContentResult(imageBytes, "image/jpg");
        }

    }
}

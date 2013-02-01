using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Bea.Core.Services;
using ImageResizer.Configuration;

namespace Bea.Web.Controllers
{
    public class PostImageController : Controller
    {
        private readonly IAdImageServices _adImageServices;

        private readonly String _postImagesFilePath;
        private readonly String _postImageExtension = ".jpg";

        public PostImageController(IAdImageServices adImageServices)
        {
            if (adImageServices == null)
                throw new ArgumentNullException("adImageServices");

            _adImageServices = adImageServices;
            _postImagesFilePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Content", "PostImages");
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

                // If file is empty or more than 1MB, skip it
                if (hpf.ContentLength == 0 || hpf.ContentLength > 1048576)
                    continue;

                // Create an image ID
                Guid id = Guid.NewGuid();

                // Compute the path to store it
                string savedFileName = Path.Combine(
                    _postImagesFilePath, 
                    String.Format("{0}{1}", id.ToString(), _postImageExtension));

                // Save the file to this path
                hpf.SaveAs(savedFileName);

                _adImageServices.StoreImage(id, false);

                imagesGuid.Add(id.ToString());
            }

            return Json(imagesGuid);
        }

    }
}

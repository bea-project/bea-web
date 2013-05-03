using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Bea.Core.Services;
using Bea.Core.Services.Ads;
using ImageResizer;
using ImageResizer.Configuration;

namespace Bea.Web.Controllers
{
    public class PostImageController : Controller
    {
        private readonly IAdImageServices _adImageServices;

        private readonly String _postImagesFilePath;
        private readonly String _postImageExtension = ".jpg";
        private readonly String _thumbnailExt = ".small";
        private readonly ResizeSettings _postImageResizeSettings;
        private readonly ResizeSettings _postImageThumbnailResizeSettings;

        public PostImageController(IAdImageServices adImageServices)
        {
            if (adImageServices == null)
                throw new ArgumentNullException("adImageServices");

            _adImageServices = adImageServices;
            _postImagesFilePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Content", "PostImages");

            // Set the default resizing settings once and for all
            _postImageResizeSettings = new ResizeSettings();
            _postImageResizeSettings.MaxWidth = 600;
            _postImageResizeSettings.MaxHeight = 400;
            _postImageResizeSettings.Format = "jpg";
            _postImageResizeSettings.Mode = FitMode.Max;

            _postImageThumbnailResizeSettings = new ResizeSettings();
            _postImageThumbnailResizeSettings.MaxWidth = 160;
            _postImageThumbnailResizeSettings.Height = 120;
            _postImageThumbnailResizeSettings.Format = "jpg";
            _postImageThumbnailResizeSettings.Mode = FitMode.Max;
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

                // Check whether we can upload the file (content type and size check)
                if (!_adImageServices.ValidateImageForUpload(hpf.ContentType, hpf.ContentLength))
                    continue;

                // Create an image ID
                Guid id = Guid.NewGuid();

                // Compute the path to store it
                string savedFileName = Path.Combine(
                    _postImagesFilePath, 
                    String.Format("{0}", id.ToString()));

                string targetFileName = String.Format("{0}{1}", savedFileName, _postImageExtension);
                string targetThumbnailFileName = String.Format("{0}{1}{2}", savedFileName, _thumbnailExt, _postImageExtension);

                // Resize to JPG for the main image
                ImageResizer.ImageBuilder.Current.Build(hpf.InputStream, targetFileName, _postImageResizeSettings);
                
                // Resize to smal JPG for the thumbnail version
                ImageResizer.ImageBuilder.Current.Build(targetFileName, targetThumbnailFileName, _postImageThumbnailResizeSettings);

                // Store the image
                _adImageServices.StoreImage(id, false);

                // Ad this image id to the result stream
                imagesGuid.Add(id.ToString());
            }

            return Json(imagesGuid);
        }
    }
}

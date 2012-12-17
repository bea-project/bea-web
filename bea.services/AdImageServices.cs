using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Domain;

namespace Bea.Services
{
    public class AdImageServices : IAdImageServices
    {
        private readonly IRepository _repository;

        public AdImageServices(IRepository repository)
        {
            _repository = repository;
        }

        public int StoreImage(String fileName, Byte[] imageBytes)
        {
            AdImage image = new AdImage();
            image.FileName = fileName;
            image.ImageBytes = imageBytes;
            //TODO: create a thumbnail

            return _repository.Save<AdImage, int>(image);
        }

        public byte[] GetAdImage(string id, bool isThumbnail)
        {
            AdImage img = _repository.Get<AdImage>(Guid.Parse(id));

            if (img == null)
                return null;

            if (isThumbnail)
                return img.ImageThumbnailBytes;
            else
                return img.ImageBytes;
        }
    }
}

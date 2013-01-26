using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
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

        public Guid StoreImage(String fileName, Byte[] imageBytes)
        {
            Guid resultId;

            using (TransactionScope scope = new TransactionScope())
            {
                AdImage image = new AdImage();
                image.FileName = fileName;
                image.ImageBytes = imageBytes;
                //TODO: create a thumbnail

                resultId = _repository.Save<AdImage, Guid>(image);

                scope.Complete();
            }

            return resultId;
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

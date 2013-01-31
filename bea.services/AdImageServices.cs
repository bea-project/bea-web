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
        private readonly IHelperService _helper;

        public AdImageServices(IRepository repository, IHelperService helper)
        {
            _repository = repository;
            _helper = helper;
        }

        public AdImage StoreImage(Guid id, Boolean isPrimary)
        {
            AdImage image = null;

            using (TransactionScope scope = new TransactionScope())
            {
                image = new AdImage();
                image.Id = id;
                image.IsPrimary = isPrimary;
                image.UploadedDate = _helper.GetCurrentDateTime();
                
                _repository.Save<AdImage>(image);

                scope.Complete();
            }

            return image;
        }

        //public byte[] GetAdImage(string id, bool isThumbnail)
        //{
        //    AdImage img = _repository.Get<AdImage>(Guid.Parse(id));

        //    if (img == null)
        //        return null;

        //    if (isThumbnail)
        //        return img.ImageThumbnailBytes;
        //    else
        //        return img.ImageBytes;
        //}
    }
}

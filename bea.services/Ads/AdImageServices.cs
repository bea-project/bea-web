using System;
using System.Text.RegularExpressions;
using System.Transactions;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Core.Services.Ads;
using Bea.Domain;
using Bea.Tools;

namespace Bea.Services.Ads
{
    public class AdImageServices : IAdImageServices
    {
        private readonly IRepository _repository;
        private readonly IHelperService _helper;
        private readonly Regex _fileExtensionMatcher = new Regex(@"\.jpe{0,1}g|\.png", RegexOptions.IgnoreCase);
        private readonly Regex _contentTypeExtensionMatcher = new Regex(@"image/jpe{0,1}g|image/png", RegexOptions.IgnoreCase);
        private readonly int _maxImageSizeInBytes = 3145728;

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

        public Boolean ValidateImageForUpload(String contentType, int imageLength)
        {
            return _contentTypeExtensionMatcher.IsMatch(contentType) 
                && imageLength > 0 
                && imageLength <= _maxImageSizeInBytes;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Domain;
using Bea.Domain.Location;
using Bea.Models;

namespace Bea.Services
{
    public class AdServices : IAdServices
    {
        private readonly IAdRepository _adRepository;
        private readonly IHelperService _helperService;
        
        public AdServices(IAdRepository adRepository, IHelperService helperService)
        {
            _adRepository = adRepository;
            _helperService = helperService;
        }

        public IDictionary<City, int> CountAdsByCities()
        {
            return _adRepository.CountAdsByCity();
        }

        public IDictionary<User, int> CountAdsByUsers()
        {
            return _adRepository.CountAdsByUser();
        }

        public IList<Ad> GetAllAds()
        {
            return _adRepository.GetAllAds();
        }

        public Ad GetAdById(long adId)
        {
            return _adRepository.GetAdById(adId);
        }

        public void DeleteAdById(long adId)
        {
            _adRepository.DeleteAdById(adId);
        }

        public void AddAd(Ad ad)
        {
            _adRepository.AddAd(ad);
        }

        public AdDetailsModel GetAdDetails(long adId)
        {
            Ad ad = _adRepository.GetAdById(adId);

            if (ad == null)
                return null;

            AdDetailsModel model = new AdDetailsModel(ad);
            model.IsNew = ad.CreationDate > _helperService.GetCurrentDateTime().AddHours(-72);

            return model;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Domain;
using Bea.Domain.Location;

namespace Bea.Services
{
    public class AdServices : IAdServices
    {
        private readonly IAdRepository _adRepository;

        public AdServices(IAdRepository adRepository)
        {
            _adRepository = adRepository;
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
    }
}

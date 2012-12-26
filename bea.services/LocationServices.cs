using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Domain.Location;
using Bea.Core.Dal;

namespace Bea.Services
{
    public class LocationServices : ILocationServices
    {
        private readonly IRepository _repository;
        private readonly ILocationRepository _locationRepository;

        public LocationServices(IRepository repository, ILocationRepository locationRepository)
        {
            _repository = repository;
            _locationRepository = locationRepository;
        }
        
        public City GetCityFromLabel(string label)
        {
            return _locationRepository.GetCityFromLabel(label);
        }

        public City GetCityFromId(int cityId)
        {
            return _locationRepository.GetCityFromId(cityId);
        }

        public IList<Province> GetAllProvinces()
        {
            return _repository.GetAll<Province>();
        }

        public IList<City> GetCitiesFromProvince(int provinceId)
        {
            return _locationRepository.GetCitiesFromProvince(provinceId).ToList();
        }
    }
}

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

        public City GetCityFromLabelUrlPart(string labelUrlPart)
        {
            return _locationRepository.GetCityFromLabelUrlPart(labelUrlPart);
        }

        public City GetCityFromId(int cityId)
        {
            return _repository.Get<City>(cityId);
        }

        public IList<Province> GetAllProvinces()
        {
            return _locationRepository.GetAllProvinces().ToList();
        }

        public IList<City> GetAllCities()
        {
            return _repository.GetAll<City>().ToList();
        }

        public IList<City> GetCitiesFromProvince(int provinceId)
        {
            return _locationRepository.GetCitiesFromProvince(provinceId).ToList();
        }

        public IList<District> GetAllDistricts()
        {
            return _repository.GetAll<District>().ToList();
        }
    }
}

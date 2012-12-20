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
        private readonly ILocationRepository _locationRepository;
        public LocationServices(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        
        public City GetCityFromLabel(string label)
        {
            return _locationRepository.GetCityFromLabel(label);
        }

        public IList<Province> GetAllProvinces()
        {
            return _locationRepository.GetAllProvinces();
        }
    }
}

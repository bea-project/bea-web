using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bea.core.dal;
using bea.core.services;
using bea.domain;
using bea.domain.location;

namespace bea.services
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
    }
}

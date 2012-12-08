using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bea.domain;
using bea.domain.location;

namespace bea.core.services
{
    public interface IAdServices
    {
        IDictionary<City, int> CountAdsByCities();
    }
}

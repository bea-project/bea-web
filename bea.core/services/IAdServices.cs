using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.location;

namespace Bea.Core.services
{
    public interface IAdServices
    {
        IDictionary<City, int> CountAdsByCities();
    }
}

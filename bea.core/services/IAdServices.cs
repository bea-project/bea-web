using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Location;

namespace Bea.Core.Services
{
    public interface IAdServices
    {
        IDictionary<City, int> CountAdsByCities();
        IList<Ad> GetAllAds();
    }
}

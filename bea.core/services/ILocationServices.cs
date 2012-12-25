using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Location;

namespace Bea.Core.Services
{
    public interface ILocationServices
    {
        City GetCityFromLabel(string label);
        City GetCityFromId(int cityId);
        IEnumerable<Province> GetAllProvinces();
        IEnumerable<City> GetCitiesFromProvince(int provinceId);
    }
}

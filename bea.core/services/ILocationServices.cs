using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Location;

namespace Bea.Core.Services
{
    public interface ILocationServices
    {
        City GetCityFromLabelUrlPart(string labelUrlPart);
        City GetCityFromId(int cityId);
        IList<Province> GetAllProvinces();
        IList<City> GetAllCities();
        IList<City> GetCitiesFromProvince(int provinceId);
        IList<District> GetAllDistricts();
    }
}

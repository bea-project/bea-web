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
        IList<Province> GetAllProvinces();
    }
}

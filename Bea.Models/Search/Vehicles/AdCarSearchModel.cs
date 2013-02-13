using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Search.Vehicles
{
    public class AdCarSearchModel : AdVehicleSearchModel
    {
        public int? BrandSelectedId { get; set; }
        public int? FuelSelectedId { get; set; }
        
    }
}

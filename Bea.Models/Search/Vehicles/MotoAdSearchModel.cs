using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Search.Vehicles
{
    public class MotoAdSearchModel : VehicleAdSearchModel
    {
        public int? BrandSelectedId { get; set; }
        public int? MinEngineSizeSelectedId { get; set; }
        public int? MaxEngineSizeSelectedId { get; set; }
    }
}

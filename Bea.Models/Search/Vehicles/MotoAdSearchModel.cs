using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Search.Vehicles
{
    public class MotoAdSearchModel : VehicleAdSearchModel
    {
        public int? BrandSelectedId { get; set; }
        public int? EngineSizeBracketSelectedId { get; set; }
    }
}

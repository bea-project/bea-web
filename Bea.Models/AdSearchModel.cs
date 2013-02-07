using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models
{
    public class AdSearchModel
    {
        public String SearchString { get; set; }
        public int? ProvinceSelectedId { get; set; }
        public int? CitySelectedId { get; set; }
        public int? CategorySelectedId { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Search.Vehicles
{
    public class CarAdSearchModel : VehicleAdSearchModel
    {
        public int? BrandSelectedId { get; set; }
        public int? FuelSelectedId { get; set; }
        public Boolean? IsAutomatic { get; set; }
    }
}

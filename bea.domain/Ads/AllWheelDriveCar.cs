﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;

namespace Bea.Domain.Ads
{
    public class AllWheelDriveCar : VehicleAd
    {
        public virtual Boolean IsAutomatic { get; set; }
        public virtual VehicleBrand Brand { get; set; }
        public virtual String OtherBrand { get; set; }
        public virtual CarFuel Fuel { get; set; }
    }
}

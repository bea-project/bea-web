using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;

namespace Bea.Domain.Ads
{
    public class OtherVehicleAd : VehicleAd
    {
        public virtual CarFuel Fuel { get; set; }

        public OtherVehicleAd()
            : base()
        {
            AdType = AdTypeEnum.OtherVehiculeAd;
        }
    }
}

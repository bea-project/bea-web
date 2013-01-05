using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;

namespace Bea.Domain.Ads
{
    public class CarAd : VehicleAd
    {
        public virtual Boolean IsAutomatic { get; set; }
        //public virtual CarBrand Brand { get; set; }
        //public virtual CarFuel Fuel { get; set; }

        public CarAd()
            : base()
        {
            AdType = AdTypeEnum.CarAd;
        }
    }
}

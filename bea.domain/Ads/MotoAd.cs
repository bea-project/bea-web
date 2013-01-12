using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;

namespace Bea.Domain.Ads
{
    public class MotoAd : VehicleAd
    {
        public virtual VehicleBrand Brand { get; set; }
        public virtual String OtherBrand { get; set; }
        public virtual int EngineSize { get; set; }

        public MotoAd()
            : base()
        {
            AdType = AdTypeEnum.MotoAd;
        }
    }
}

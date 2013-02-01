using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;

namespace Bea.Domain.Ads.WaterSport
{
    public class WaterSportAd : BaseAd
    {
        public virtual WaterSportType Type { get; set; }
    }
}

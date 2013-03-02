using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads.WaterSport;

namespace Bea.Models.Details.WaterSport
{
    public class WaterSportAdDetailsModel : AdDetailsModel
    {
        public String SportType { get; set; }

        public WaterSportAdDetailsModel(WaterSportAd ad)
            : base(ad)
        {
            this.SportType = ad.Type == null ? String.Empty : ad.Type.Label;
        }
    }
}

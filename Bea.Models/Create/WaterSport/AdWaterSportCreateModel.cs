using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Bea.Domain.Ads;
using Bea.Domain.Ads.WaterSport;

namespace Bea.Models.Create.WaterSport
{
    public class AdWaterSportCreateModel : AdCreateModel
    {
        [DisplayName("Discipline:")]
        public int? SelectedTypeId { get; set; }

        public int Type { get; set; }

        public AdWaterSportCreateModel()
        {
            this.Type = this.Type = (int)AdTypeEnum.WaterSportAd;
        }

        public AdWaterSportCreateModel(WaterSportAd ad)
            : base(ad)
        {
            if (ad.Type != null)
                this.SelectedTypeId = ad.Type.Id;

            this.Type = (int)AdTypeEnum.WaterSportAd;
        }
    }  
}

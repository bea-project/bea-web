using Bea.Domain.Ads;
using Bea.Domain.Ads.WaterSport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Bea.Models.Create.WaterSport
{
    public class AdMotorBoatEngineCreateModel : AdCreateModel
    {
        [DisplayName("Type:")]
        public int? SelectedTypeId { get; set; }

        [DisplayName("Annee-Modele:")]
        public int? SelectedYearId { get; set; }

        [DisplayName("Puissance Moteur:")]
        public int? Hp { get; set; }

        public int Type { get; set; }

        public AdMotorBoatEngineCreateModel()
        {
            this.Type = this.Type = (int)AdTypeEnum.MotorBoatEngineAd;
        }

        public AdMotorBoatEngineCreateModel(MotorBoatEngineAd ad)
            : base(ad)
        {
            
            this.Hp = ad.Hp;
            
            if (ad.Type != null)
                this.SelectedTypeId = ad.Type.Id;

            if (ad.Year != 0)
                this.SelectedYearId = ad.Year;
            this.Type = (int)AdTypeEnum.MotorBoatEngineAd;
        }
    }
}

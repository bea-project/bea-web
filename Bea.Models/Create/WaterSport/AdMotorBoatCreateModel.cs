using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Bea.Domain.Ads;
using Bea.Domain.Ads.WaterSport;

namespace Bea.Models.Create.WaterSport
{
    public class AdMotorBoatCreateModel : AdCreateModel
    {
        [DisplayName("Type:")]
        public int? SelectedTypeId { get; set; }

        [DisplayName("Longueur:")]
        public Decimal? Length { get; set; }

        [DisplayName("Moteur:")]
        public int? SelectedMotorTypeId { get; set; }

        [DisplayName("Annee-Modele:")]
        public int? SelectedYearId { get; set; }

        [DisplayName("Heures Moteur:")]
        public int? NbHours { get; set; }

        [DisplayName("Puissance Moteur:")]
        public int? Hp { get; set; }
        
        public int Type { get; set; }

        public AdMotorBoatCreateModel()
            : base()
        {
            this.Type = this.Type = (int)AdTypeEnum.MotorBoatAd;
        }

        public AdMotorBoatCreateModel(MotorBoatAd ad)
            : base(ad)
        {
            this.Hp = ad.Hp;
            this.Length = ad.Length;

            if (ad.Type != null)
                this.SelectedTypeId = ad.Type.Id;
            if (ad.MotorType != null)
                this.SelectedMotorTypeId = ad.MotorType.Id;

            if (ad.Year != 0)
                this.SelectedYearId = ad.Year;
            this.Type = (int)AdTypeEnum.MotorBoatAd;
        }
    }
}

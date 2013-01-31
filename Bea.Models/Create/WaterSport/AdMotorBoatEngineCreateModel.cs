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

        [DisplayName("Heures Moteur:")]
        public int? NbHours { get; set; }

        [DisplayName("Puissance Moteur:")]
        public int? Hp { get; set; }

        [DisplayName("Cylindres:")]
        public int? NbCylinders { get; set; }

        public int Type { get; set; }

        public AdMotorBoatEngineCreateModel()
        {
            this.Type = this.Type = (int)AdTypeEnum.MotorBoatEngineAd;
        }

        public AdMotorBoatEngineCreateModel(MotorBoatEngineAd ad)
        {
            this.Body = ad.Body;
            this.IsOffer = ad.IsOffer;
            this.Price = ad.Price;
            if (ad.Category != null)
                this.SelectedCategoryId = ad.Category.Id;
            if (ad.City != null)
            {
                this.SelectedCityId = ad.City.Id;
                this.SelectedProvinceId = ad.City.Province.Id;
            }
            this.Telephone = ad.PhoneNumber;
            this.Title = ad.Title;

            this.Hp = ad.Hp;
            this.NbHours = ad.NbHours;
            this.NbCylinders= ad.NbCylinder;

            if (ad.Type != null)
                this.SelectedTypeId = ad.Type.Id;

            if (ad.Year != 0)
                this.SelectedYearId = ad.Year;
            this.Type = (int)AdTypeEnum.MotorBoatEngineAd;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Bea.Domain.Ads;

namespace Bea.Models.Create.WaterSport
{
    public class AdMotorBoatCreateModel : AdCreateModel
    {
        [DisplayName("Type:")]
        public int? SelectedTypeId { get; set; }

        [DisplayName("Longueur:")]
        public Decimal Length { get; set; }

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
        {
            this.Type = this.Type = (int)AdTypeEnum.MotorBoatAd;
        }

        public AdMotorBoatCreateModel(MotorBoatAd ad)
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
            this.Length = ad.Length;
            this.NbHours = ad.NbHours;

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

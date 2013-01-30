using Bea.Domain.Ads;
using Bea.Domain.Ads.WaterSport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Bea.Models.Create.WaterSport
{
    public class AdSailingBoatCreateModel : AdCreateModel
    {
        [DisplayName("Coque:")]
        public int? SelectedHullTypeId { get; set; }
        
        [DisplayName("Longueur:")]
        public Decimal? Length { get; set; }

        [DisplayName("Annee-Modele:")]
        public int? SelectedYearId { get; set; }

        [DisplayName("Matériaux:")]
        public int? SelectedTypeId { get; set; }

        public int Type { get; set; }

        public AdSailingBoatCreateModel()
        {
            this.Type = this.Type = (int)AdTypeEnum.SailingBoatAd;
        }

        public AdSailingBoatCreateModel(SailingBoatAd ad)
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

            this.Length = ad.Length;

            if (ad.HullType != null)
                this.SelectedHullTypeId = ad.HullType.Id;
            if (ad.Type != null)
                this.SelectedTypeId = ad.Type.Id;

            if (ad.Year != 0)
                this.SelectedYearId = ad.Year;
            this.Type = (int)AdTypeEnum.SailingBoatAd;
        }

    }
}

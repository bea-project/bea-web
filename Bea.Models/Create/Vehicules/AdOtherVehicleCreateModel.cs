using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Bea.Domain.Ads;

namespace Bea.Models.Create.Vehicules
{
    public class AdOtherVehicleCreateModel : AdVehicleCreateModel
    {
        [DisplayName("Carburant:")]
        public int? SelectedFuelId { get; set; }

        //public int Type { get; set; }

        public AdOtherVehicleCreateModel()
        {
            this.Type = (int)AdTypeEnum.OtherVehiculeAd;
        }

        public AdOtherVehicleCreateModel(OtherVehicleAd ad)
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
            this.Km = ad.Kilometers;
            if(ad.Fuel!=null)
                this.SelectedFuelId = ad.Fuel.Id;
            if (ad.Year != 0)
                this.SelectedYearId = ad.Year;
            this.Type = (int)AdTypeEnum.OtherVehiculeAd;
        }

    }
}

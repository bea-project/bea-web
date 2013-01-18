using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Bea.Domain.Ads;

namespace Bea.Models.Create.Vehicules
{
    public class AdOtherVehicleCreateModel : AdVehiculeCreateModel
    {
        [DisplayName("Carburant:")]
        public int? SelectedFuelId { get; set; }

        public int Type { get; set; }

        public AdOtherVehicleCreateModel()
        {
            this.Type = (int)AdTypeEnum.OtherVehiculeAd;
        }

        public AdOtherVehicleCreateModel(OtherVehicleAd ad, AdCreateModel model)
        {
            this.Body = ad.Body;
            this.IsOffer = ad.IsOffer;
            this.Price = ad.Price;
            this.SelectedCategoryId = model.SelectedCategoryId;
            this.SelectedCityId = model.SelectedCityId;
            this.SelectedProvinceId = model.SelectedProvinceId;
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

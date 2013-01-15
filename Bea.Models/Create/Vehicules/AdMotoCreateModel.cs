using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using Bea.Domain.Ads;

namespace Bea.Models.Create.Vehicules
{
    public class AdMotoCreateModel : AdVehiculeCreateModel
    {
        [DisplayName("Marque:")]
        public int? SelectedBrandId { get; set; }

        [DisplayName("Cylindrée:")]
        public int EngineSize { get; set; }

        public AdMotoCreateModel()
        { }

        public AdMotoCreateModel(MotoAd ad, AdCreateModel model)
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
            if (ad.Brand != null)
                this.SelectedBrandId = ad.Brand.Id;
            this.EngineSize= ad.EngineSize;
        }

    }
}

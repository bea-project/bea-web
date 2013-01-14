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
    public class AdMotoCreateModel : AdCreateModel
    {
        //public String Brand { get; set; }


        public int? SelectedBrandId { get; set; }
        [DisplayName("Marque:")]
        public IEnumerable<SelectListItem> BrandsList { get; set; }

        public int? SelectedFuelId { get; set; }
        [DisplayName("Carburant:")]
        public IEnumerable<SelectListItem> FuelList { get; set; }

        public AdMotoCreateModel()
        { }

        //public AdCarCreateModel(CarAd ad, AdCreateModel model)
        //{
        //    this.Body = ad.Body;
        //    this.IsOffer = ad.IsOffer;
        //    this.Price = ad.Price;
        //    this.SelectedCategoryId = model.SelectedCategoryId;
        //    this.SelectedCityId = model.SelectedCityId;
        //    this.SelectedProvinceId = model.SelectedProvinceId;
        //    this.Telephone = ad.PhoneNumber;
        //    this.Title = ad.Title;
        //    this.Km = ad.Kilometers;
        //    if (ad.Fuel != null)
        //        this.SelectedFuelId = ad.Fuel.Id;
        //    if (ad.Brand != null)
        //        this.SelectedBrandId = ad.Brand.Id;
        //}

    }
}

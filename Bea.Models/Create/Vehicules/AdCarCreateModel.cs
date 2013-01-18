﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Bea.Domain.Ads;

namespace Bea.Models.Create.Vehicules
{
    public class AdCarCreateModel : AdVehiculeCreateModel
    {  
        [DisplayName("Marque:")]
        public int? SelectedBrandId { get; set; }
        
        [DisplayName("Carburant:")]
        public int? SelectedFuelId { get; set; }

        [DisplayName("Boîte de vitesse:")]
        public bool IsAutomatic { get; set; }

        public int Type { get; set; }

        public AdCarCreateModel()
        {
            this.Type = (int)AdTypeEnum.CarAd;
        }

        public AdCarCreateModel( CarAd ad, AdCreateModel model)
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
            if (ad.Brand != null)
                this.SelectedBrandId = ad.Brand.Id;
            if (ad.Year != 0)
                this.SelectedYearId = ad.Year;
            this.Type = (int)AdTypeEnum.CarAd;
        }

    }
}

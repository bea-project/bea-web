﻿using System;
using Bea.Domain.Ads;

namespace Bea.Models.Details
{
    public class CarAdDetailsModel : AdDetailsModel
    {
        #region CarAd properties

        public int? Year { get; set; }
        public int? Kilometers { get; set; }
        public String GearType { get; set; }
        public String Brand { get; set; }
        public String Fuel { get; set; }

        #endregion

        public CarAdDetailsModel(CarAd ad)
            : base(ad as BaseAd)
        {
            Year = ad.Year;
            Kilometers = ad.Kilometers;
            GearType = ad.IsAutomatic ? "Automatique" : "Manuelle";
            Brand = ad.Brand == null ? String.Empty : ad.Brand.Label;
            Fuel = ad.Fuel == null ? String.Empty : ad.Fuel.Label;
        }
    }
}

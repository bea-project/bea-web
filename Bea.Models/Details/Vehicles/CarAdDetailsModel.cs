using System;
using Bea.Domain.Ads;

namespace Bea.Models.Details.Vehicles
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
            Fuel = ad.Fuel == null ? String.Empty : ad.Fuel.Label;

            if (!String.IsNullOrEmpty(ad.OtherBrand))
                Brand = ad.OtherBrand;
            else
                Brand = ad.Brand == null ? String.Empty : ad.Brand.Label;
        }
    }
}

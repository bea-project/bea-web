using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Models.Details
{
    public class OtherVehicleAdDetailsModel : AdDetailsModel
    {
        #region OtherVehicleAd properties

        public int? Year { get; set; }
        public int? Kilometers { get; set; }
        public String Fuel { get; set; }

        #endregion

        public OtherVehicleAdDetailsModel(OtherVehicleAd ad)
            : base(ad as BaseAd)
        {
            Year = ad.Year;
            Kilometers = ad.Kilometers;
            Fuel = ad.Fuel == null ? String.Empty : ad.Fuel.Label;
        }
    }
}

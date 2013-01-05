using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Models.Details
{
    public class CarAdDetailsModel : AdDetailsModel
    {
        #region CarAd properties

        public int? Year { get; set; }
        public int? Kilometers { get; set; }
        public String GearType { get; set; }

        #endregion

        public CarAdDetailsModel(CarAd ad)
            : base(ad as BaseAd)
        {
            Year = ad.Year;
            Kilometers = ad.Kilometers;
            GearType = ad.IsAutomatic ? "Automatique" : "Manuelle";
        }
    }
}

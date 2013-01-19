using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Models.Details
{
    public class MotoAdDetailsModel : AdDetailsModel
    {
        #region MotoAd properties

        public int? Year { get; set; }
        public int? Kilometers { get; set; }
        public int EngineSize { get; set; }
        public String Brand { get; set; }

        #endregion

        public MotoAdDetailsModel(MotoAd ad)
            : base(ad as BaseAd)
        {
            Year = ad.Year;
            Kilometers = ad.Kilometers;
            EngineSize = ad.EngineSize;

            if (!String.IsNullOrEmpty(ad.OtherBrand))
                Brand = ad.OtherBrand;
            else
                Brand = ad.Brand == null ? String.Empty : ad.Brand.Label;
        }
    }
}

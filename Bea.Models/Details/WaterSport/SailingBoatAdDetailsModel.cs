using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads.WaterSport;
using Bea.Tools;

namespace Bea.Models.Details.WaterSport
{
    public class SailingBoatAdDetailsModel : AdDetailsModel
    {
        public String BoatType { get; set; }
        public String HullType { get; set; }
        public int? Year { get; set; }
        public String Length { get; set; }

        public SailingBoatAdDetailsModel(SailingBoatAd ad, IHelperService helper)
            : base(ad)
        {
            BoatType = ad.SailingBoatType != null ? ad.SailingBoatType.Label : String.Empty;
            HullType = ad.HullType != null ? ad.HullType.Label : String.Empty;
            Year = ad.Year != 0 ? ad.Year : (int?) null;
            Length = ad.Length != 0 ? String.Format(helper.GetCulture(), "{0:F2} Mtr", ad.Length) : String.Empty;
        }
    }
}

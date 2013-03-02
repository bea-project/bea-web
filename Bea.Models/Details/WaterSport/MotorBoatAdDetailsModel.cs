using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads.WaterSport;
using Bea.Tools;

namespace Bea.Models.Details.WaterSport
{
    public class MotorBoatAdDetailsModel : AdDetailsModel
    {
        public String BoatType { get; set; }
        public String MotorType { get; set; }
        public int? Year { get; set; }
        public String Length { get; set; }
        public String Hp { get; set; }

        public MotorBoatAdDetailsModel(MotorBoatAd ad, IHelperService helper)
            : base(ad)
        {
            BoatType = ad.Type != null ? ad.Type.Label : String.Empty;
            MotorType = ad.MotorType != null ? ad.MotorType.Label : String.Empty;
            Year = ad.Year != 0 ? ad.Year : (int?) null;
            Length = ad.Length != 0 ? String.Format(helper.GetCulture(), "{0:F2} Mtr", ad.Length) : String.Empty;
            Hp = ad.Hp != 0 ? String.Format("{0} Cv", ad.Hp) : String.Empty;
        }
    }
}

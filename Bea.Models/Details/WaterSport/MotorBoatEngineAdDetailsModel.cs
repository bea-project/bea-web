using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads.WaterSport;

namespace Bea.Models.Details.WaterSport
{
    public class MotorBoatEngineAdDetailsModel : AdDetailsModel
    {
        public String MotorType { get; set; }
        public int? Year { get; set; }
        public String Hp { get; set; }

        public MotorBoatEngineAdDetailsModel(MotorBoatEngineAd ad)
            : base(ad)
        {
            MotorType = ad.Type != null ? ad.Type.Label : String.Empty;
            Year = ad.Year != 0 ? ad.Year : (int?)null;
            Hp = ad.Hp != 0 ? String.Format("{0} Cv", ad.Hp) : String.Empty;
        }
    }
}

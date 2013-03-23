using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Models.Search.Vehicles
{
    public class VehicleAdSearchModel : AdSearchModel
    {
        public int? AgeBracketSelectedId { get; set; }
        public int? KmBracketSelectedId { get; set; }
        public int Type { get; set; }

        public VehicleAdSearchModel()
        {
            this.Type = (int)AdTypeEnum.VehiculeAd;
        }
    }
}

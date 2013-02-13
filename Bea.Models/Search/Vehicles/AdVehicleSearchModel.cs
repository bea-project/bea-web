using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Models.Search.Vehicles
{
    public class AdVehicleSearchModel : AdSearchModel
    {
        public int? MinYearSelectedId { get; set; }
        public int? MaxYearSelectedId { get; set; }
        public int? MinKmSelectedId { get; set; }
        public int? MaxKmSelectedId { get; set; }
        public int Type { get; set; }

        public AdVehicleSearchModel()
        {
            this.Type = (int)AdTypeEnum.VehiculeAd;
        }
    }
}

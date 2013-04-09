using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Core.Dal
{
    public class AdSearchParameters
    {
        public string[] AndSearchStrings { get; set; }
        public int? CityId { get; set; }
        public int[] CategoryIds { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }

        public int? MinKm { get; set; }
        public int? MaxKm { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? BrandId { get; set; }
        public int? FueldId { get; set; }
        public Boolean? IsAuto { get; set; }
        public int? MinEngineSize { get; set; }
        public int? MaxEngineSize { get; set; }

        public int? MinNbRooms { get; set; }
        public int? MaxNbRooms { get; set; }
        public int? RealEstateTypeId { get; set; }
        public int? DistrictId { get; set; }
        public Boolean? IsFurnished { get; set; }
        public int? MinSurfaceArea { get; set; }
        public int? MaxSurfaceArea { get; set; }

        public int? WaterTypeId { get; set; }
        public int? MotorEngineTypeId { get; set; }
        public int? HullTypeId { get; set; }
        public int? MotorBoatTypeId { get; set; }
        public int? SailingBoatTypeId { get; set; }
        public int? MinHp { get; set; }
        public int? MaxHp { get; set; }
        public double? MinLength { get; set; }
        public double? MaxLength { get; set; }
    }
}

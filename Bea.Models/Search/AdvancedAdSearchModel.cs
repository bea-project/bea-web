using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Search
{
    public class AdvancedAdSearchModel : AdSearchModel
    {
        // Vehicle specific properties
        public int? AgeBracketSelectedId { get; set; }
        public int? KmBracketSelectedId { get; set; }

        // Car specific properties
        public int? BrandSelectedId { get; set; }
        public int? FuelSelectedId { get; set; }
        public Boolean? IsAutomatic { get; set; }

        // Moto specific properties
        public int? EngineSizeBracketSelectedId { get; set; }

        // Real Estate
        public int? MinNbRooms { get; set; }
        public int? MaxNbRooms { get; set; }
        public int? SelectedRealEstateTypeId { get; set; }
        public int? SelectedDistrictId { get; set; }

        //TODO: later v--------------------------v

        // WaterSport
        public int? SelectedHullTypeId { get; set; }
        public int? MinYearSelectedId { get; set; }
        public int? MaxYearSelectedId { get; set; }
        public int? MinLengthSelectedId { get; set; }
        public int? MaxLengthSelectedId { get; set; }
        public int? MinHpSelectedId { get; set; }
        public int? MaxHpSelectedId { get; set; }
        public int? SelectedMotorTypeId { get; set; }

        //TODO: later ^--------------------------^
    }
}

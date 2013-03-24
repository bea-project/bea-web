using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models.Search.Vehicles;

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


        //TODO: later v--------------------------v

        // Real Estate
        public int? MinRoomNbSelectedId { get; set; }
        public int? MaxRoomNbSelectedId { get; set; }
        public int? SelectedTypeId { get; set; }
        public int? SelectedDistrictId { get; set; }

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

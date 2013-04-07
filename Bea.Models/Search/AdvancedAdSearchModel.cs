using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Search
{
    public class AdvancedAdSearchModel : AdSearchModel
    {
        // Price related info
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }

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
        public int? NbRoomsBracketSelectedId { get; set; }
        public int? SelectedRealEstateTypeId { get; set; }
        public int? SelectedDistrictId { get; set; }
        public int? SurfaceAreaBracketSelectedId { get; set; }
        public Boolean? IsFurnished { get; set; }

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

        public AdvancedAdSearchModel()
        {

        }

        public AdvancedAdSearchModel(AdSearchModel searchModel)
            : base(searchModel)
        {

        }

        public AdvancedAdSearchModel(AdvancedAdSearchModel searchModel)
            : base(searchModel)
        {
            this.MinPrice = searchModel.MinPrice;
            this.MaxPrice = searchModel.MaxPrice;

            this.AgeBracketSelectedId = searchModel.AgeBracketSelectedId;
            this.KmBracketSelectedId = searchModel.KmBracketSelectedId;

            this.BrandSelectedId = searchModel.BrandSelectedId;
            this.FuelSelectedId = searchModel.FuelSelectedId;
            this.IsAutomatic = searchModel.IsAutomatic;

            this.EngineSizeBracketSelectedId = searchModel.EngineSizeBracketSelectedId;

            this.NbRoomsBracketSelectedId = searchModel.NbRoomsBracketSelectedId;
            this.SelectedRealEstateTypeId = searchModel.SelectedRealEstateTypeId;
            this.SelectedDistrictId = searchModel.SelectedDistrictId;
            this.SurfaceAreaBracketSelectedId = searchModel.SurfaceAreaBracketSelectedId;
            this.IsFurnished = searchModel.IsFurnished;

            this.SelectedHullTypeId = searchModel.SelectedHullTypeId;
            this.MinYearSelectedId = searchModel.MinYearSelectedId;
            this.MaxYearSelectedId = searchModel.MaxYearSelectedId;
            this.MinLengthSelectedId = searchModel.MinLengthSelectedId;
            this.MaxLengthSelectedId = searchModel.MaxLengthSelectedId;
            this.MinHpSelectedId = searchModel.MinHpSelectedId;
            this.MaxHpSelectedId = searchModel.MaxHpSelectedId;
            this.SelectedMotorTypeId = searchModel.SelectedMotorTypeId;
        }
    }
}

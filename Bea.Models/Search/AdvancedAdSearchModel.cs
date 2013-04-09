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

        // WaterSport
        public int? SelectedWaterTypeId { get; set; }
        public int? SelectedMotorBoatTypeId { get; set; }
        public int? SelectedMotorTypeId { get; set; }
        public int? SelectedSailingBoatTypeId { get; set; }
        public int? SelectedHullTypeId { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public int? MinHp { get; set; }
        public int? MaxHp { get; set; }

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

            this.SelectedWaterTypeId = searchModel.SelectedWaterTypeId;
            this.SelectedMotorBoatTypeId = searchModel.SelectedMotorBoatTypeId;
            this.SelectedSailingBoatTypeId = searchModel.SelectedSailingBoatTypeId;
            this.SelectedHullTypeId = searchModel.SelectedHullTypeId;
            this.MinLength = searchModel.MinLength;
            this.MaxLength = searchModel.MaxLength;
            this.MinHp = searchModel.MinHp;
            this.MaxHp = searchModel.MaxHp;
            this.SelectedMotorTypeId = searchModel.SelectedMotorTypeId;
        }
    }
}

using Bea.Domain.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Create
{
    public class AdvancedAdCreateModel
    {
        //Type of the ad
        public AdTypeEnum Type { get; set; }

        //Common Ad
        public String Title { get; set; }
        public String Body { get; set; }
        public Double? Price { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Telephone { get; set; }
        public int? SelectedProvinceId { get; set; }
        public int? SelectedCityId { get; set; }
        public Boolean IsOffer { get; set; }
        public int? SelectedCategoryId { get; set; }
        public String ImageIds { get; set; }

        //Real Estate
        public int? RoomNb { get; set; }
        public int? SelectedRealEstateTypeId { get; set; }
        public int? SurfaceArea { get; set; }
        public Boolean? IsFurnished { get; set; }
        public int? SelectedDistrictId { get; set; }

        //Vehicle
        public int? Km { get; set; }
        public int? SelectedYearId { get; set; }
        public int? SelectedCarBrandId { get; set; }
        public int? SelectedMotoBrandId { get; set; }
        public int? SelectedFuelId { get; set; }
        public Boolean? IsAutomatic { get; set; }
        public int? EngineSize { get; set; }

        //WaterSport
        public Double? Length { get; set; }
        public int? SelectedMotorTypeId { get; set; }
        public int? Hp { get; set; }
        public int? SelectedHullTypeId { get; set; }
        public int? SelectedWaterSportTypeId { get; set; }
        public int? SelectedSailingBoatTypeId { get; set; }
        public int? SelectedMotorBoatEngineTypeId { get; set; }
        public int? SelectedMotorBoatTypeId { get; set; }

    }
}

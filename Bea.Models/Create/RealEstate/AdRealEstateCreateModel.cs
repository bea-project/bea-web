using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Bea.Domain.Ads;

namespace Bea.Models.Create.RealEstate
{
    public class AdRealEstateCreateModel : AdCreateModel
    {
        [DisplayName("Nombre de pieces:")]
        public int? RoomNb { get; set; }

        [DisplayName("Type de bien:")]
        public int? SelectedTypeId { get; set; }

        [DisplayName("Superficie:")]
        public int? SurfaceArea { get; set; }

        [DisplayName("Meublé:")]
        public Boolean? IsFurnished { get; set; }

        [DisplayName("Quartier:")]
        public int? SelectedDistrictId { get; set; }

        public int Type { get; set; }

        public AdRealEstateCreateModel()
        {
            this.Type = (int)AdTypeEnum.RealEstateAd;
        }

        public AdRealEstateCreateModel(RealEstateAd ad)
            : base(ad)
        {
            this.Type = (int)AdTypeEnum.RealEstateAd;
            if (ad.Type != null)
                this.SelectedTypeId = ad.Type.Id;
            if (ad.District != null)
                this.SelectedDistrictId = ad.District.Id;
            this.RoomNb = ad.RoomsNumber;
            this.SurfaceArea = ad.SurfaceArea;
            this.IsFurnished = ad.IsFurnished;
        }


    }
}

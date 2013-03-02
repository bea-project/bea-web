using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Models.Details.RealEstate
{
    public class RealEstateAdDetailsModel : AdDetailsModel
    {
        public int? RoomNb { get; set; }
        public String Type { get; set; }
        public String SurfaceArea { get; set; }
        public String Furnished { get; set; }
        public String District { get; set; }

        public RealEstateAdDetailsModel(RealEstateAd ad)
            : base(ad)
        {
            RoomNb = ad.RoomsNumber != 0 ? ad.RoomsNumber : (int?) null;
            Type = ad.Type != null ? ad.Type.Label : String.Empty;
            District = ad.District != null ? ad.District.Label : String.Empty;
            SurfaceArea = ad.SurfaceArea != 0 ? String.Format("{0} m²", ad.SurfaceArea) : String.Empty;
            Furnished = ad.IsFurnished.HasValue ? (ad.IsFurnished.Value ? "Oui" : "Non") : String.Empty;
        }
    }
}

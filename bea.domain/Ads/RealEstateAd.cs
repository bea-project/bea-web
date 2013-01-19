using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;

namespace Bea.Domain.Ads
{
    public class RealEstateAd : BaseAd
    {
        public virtual int RoomsNumber { get; set; }
        public virtual int SurfaceArea { get; set; }
        public virtual int FloorNumber { get; set; }
        public virtual Boolean IsFurnished { get; set; }
        public virtual RealEstateType RealEstateType { get; set; }

        public RealEstateAd()
            : base()
        {
        }
    }
}

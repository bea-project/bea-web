using Bea.Domain.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.Ads.WaterSport
{
    public class SailingBoatAd : BaseAd
    {
        public virtual SailingBoatType Type { get; set; }
        public virtual Decimal Length { get; set; }
        public virtual int Year { get; set; }
        public virtual SailingBoatHullType HullType { get; set; }
    }
}

using Bea.Domain.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.Ads.WaterSport
{
    public class SailingBoatAd : BaseAd
    {
        public virtual SailingBoatType SailingBoatType { get; set; }
        public virtual double Length { get; set; }
        public virtual int Year { get; set; }
        public virtual SailingBoatHullType HullType { get; set; }
    }
}

using Bea.Domain.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.Ads.WaterSport
{
    public class MotorBoatEngineAd : BaseAd
    {
        public virtual MotorBoatEngineType Type { get; set; }
        public virtual int Hp { get; set; }
        public virtual int Year { get; set; }
        public virtual int NbCylinder { get; set; }
        
    }
}

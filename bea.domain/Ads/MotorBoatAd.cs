using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;

namespace Bea.Domain.Ads
{
    public class MotorBoatAd : BaseAd
    {
        public virtual MotorBoatType Type { get; set; }
        public virtual int NbHours { get; set; }
        public virtual Decimal Length { get; set; }
        public virtual int Year { get; set; }
        public virtual MotorBoatEngineType MotorType { get; set; }
        public virtual int Hp { get; set; }
    }
}

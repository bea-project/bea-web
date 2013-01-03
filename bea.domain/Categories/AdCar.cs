using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Domain.Categories
{
    public class AdCar : Ad
    {
        public virtual String Brand { get; set; }
        public virtual int Km { get; set; }
    }
}

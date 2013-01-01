using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.Category
{
    public class AdCar : Ad
    {
        public virtual String Brand { get; set; }
        public virtual int Km { get; set; }
    }
}

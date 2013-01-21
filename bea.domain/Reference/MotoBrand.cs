using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.Reference
{
    public class MotoBrand
    {
        public virtual int Id { get; set; }
        public virtual String Label { get; set; }
        public virtual Boolean IsMainBrand { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.Reference
{
    public class BracketItemReference
    {
        public int Id { get; set; }
        public String Label { get; set; }
        public int LowValue { get; set; }
        public int HighValue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Domain.Categories
{
    class AdKite : Ad
    {
        public virtual String Brand { get; set; }
        public virtual int Surface { get; set; }
    }
}

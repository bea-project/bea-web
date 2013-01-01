using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.Category
{
    class AdKite : Ad
    {
        public virtual String Brand { get; set; }
        public virtual int Surface { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Bea.Domain.Location
{
    public class District
    {
        public virtual int Id { get; set; }
        public virtual String Label { get; set; }
        public virtual City City { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Location;

namespace Bea.Domain
{
    public class Ad
    {
        public virtual int Id { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual List<string> Pictures { get; set; }
        public virtual City City { get; set; }
    }
}

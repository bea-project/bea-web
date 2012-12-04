using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bea.dal.entities
{
    public class Ad
    {
        public Ad() { }
        public virtual int adId { get; set; }
        public virtual User createdBy { get; set; }
        public virtual string title { get; set; }
        public virtual string body { get; set; }
    }
}

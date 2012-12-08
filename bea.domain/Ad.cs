using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bea.domain.location;

namespace bea.domain
{
    public class Ad
    {
        public Ad() {
            this.creationDate = DateTime.Now;
        }
        public virtual int adId { get; set; }
        public virtual User createdBy { get; set; }
        public virtual DateTime creationDate { get; set; }
        public virtual string title { get; set; }
        public virtual string body { get; set; }
        public virtual List<string> pictures { get; set; }
        public virtual City location { get; set; }
    }
}

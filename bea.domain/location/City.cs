using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bea.domain.location
{
    public class City
    {
        public City()
        {
            this.ads = new List<Ad>();
        }
        public virtual int cityId { get; set; }
        public virtual string label { get; set; }
        public virtual Province province { get; set; }
        public virtual IList<Ad> ads { get; set; }
        //public virtual IList<District> districts { get; set; }

        public virtual void AddAd(Ad ad)
        {
            this.ads.Add(ad);
            ad.location = this;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Domain.Location
{
    public class City
    {
        public City()
        {
            this.Ads = new List<Ad>();
        }

        public virtual int Id { get; set; }
        public virtual string Label { get; set; }
        public virtual Province Province { get; set; }
        public virtual IList<Ad> Ads { get; set; }

        public virtual void AddAd(Ad ad)
        {
            this.Ads.Add(ad);
            ad.City = this;
        }
    }
}

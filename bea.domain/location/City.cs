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
            this.Ads = new List<BaseAd>();
            this.Districts = new List<District>();
        }

        public virtual int Id { get; set; }
        public virtual string Label { get; set; }
        public virtual Province Province { get; set; }
        public virtual IList<BaseAd> Ads { get; set; }
        public virtual IList<District> Districts { get; set; }

        public virtual void AddAd(BaseAd ad)
        {
            this.Ads.Add(ad);
            ad.City = this;
        }

        public virtual void AddDistrict(District district)
        {
            this.Districts.Add(district);
            district.City = this;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Categories;
using Bea.Domain.Location;

namespace Bea.Domain.Ads
{
    public class BaseAd
    {
        public virtual long Id { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual City City { get; set; }
        public virtual Double Price { get; set; }
        public virtual Boolean IsOffer { get; set; }
        public virtual Category Category { get; set; }
        public virtual String PhoneNumber { get; set; }
        
        public virtual IList<AdImage> Images { get; set; }

        public virtual Boolean IsActivated { get; set; }
        public virtual String ActivationToken { get; set; }

        protected BaseAd()
        {
            Images = new List<AdImage>();
        }

        public virtual void AddImage(AdImage adImage)
        {
            Images.Add(adImage);
            adImage.BaseAd = this;
        }
    }
}

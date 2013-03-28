using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Reference;

namespace Bea.Domain.Ads
{
    public class BaseAd
    {
        public virtual long Id { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual String Title { get; set; }
        public virtual String Body { get; set; }
        public virtual City City { get; set; }
        public virtual Double Price { get; set; }
        public virtual Boolean IsOffer { get; set; }
        public virtual Category Category { get; set; }
        public virtual String PhoneNumber { get; set; }
        
        public virtual IList<AdImage> Images { get; set; }

        public virtual Boolean IsActivated { get; set; }
        public virtual String ActivationToken { get; set; }

        public virtual Boolean IsDeleted { get; set; }
        public virtual DateTime DeletionDate { get; set; }
        public virtual DeletionReason DeletedReason { get; set; }

        public BaseAd()
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

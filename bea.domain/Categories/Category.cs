using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Domain.Categories
{
    public class Category
    {
        public Category()
        {
            this.Ads = new List<BaseAd>();
        }
        
        public virtual int Id { get; set; }
        public virtual String Label { get; set; }
        public virtual CategoryGroup CategoryGrp { get; set; }
        public virtual IList<BaseAd> Ads { get; set; }
        public virtual AdTypeEnum Type { get; set; }

        //Add the Ad to the category and set the Ad category to this
        public virtual void AddAd(BaseAd adToBeAdded)
        {
            this.Ads.Add(adToBeAdded);
            adToBeAdded.Category = this;
        }
    }
}

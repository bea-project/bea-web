using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.Category
{
    public class CategoryElement
    {
        public CategoryElement()
        {
            this.Ads = new List<Ad>();
        }
        
        public virtual int Id { get; set; }
        public virtual String Label { get; set; }
        public virtual CategoryGroup CategoryGrp { get; set; }
        public virtual IList<Ad> Ads { get; set; }

        //Add the Ad to the category and set the Ad category to this
        public virtual void AddAd(Ad adToBeAdded)
        {
            this.Ads.Add(adToBeAdded);
            adToBeAdded.Category = this;
        }
    }
}

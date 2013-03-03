using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Domain.Categories
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual String Label { get; set; }
        public virtual String LabelUrlPart { get; set; }
        public virtual IList<BaseAd> Ads { get; set; }
        public virtual AdTypeEnum Type { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual IList<Category> SubCategories { get; set; }

        public Category()
        {
            this.Ads = new List<BaseAd>();
            this.SubCategories = new List<Category>();
        }

        //Add the Ad to the category and set the Ad category to this
        public virtual void AddAd(BaseAd adToBeAdded)
        {
            this.Ads.Add(adToBeAdded);
            adToBeAdded.Category = this;
        }

        public virtual void AddCategory(Category categoryToBeAdded)
        {
            categoryToBeAdded.ParentCategory = this;
            this.SubCategories.Add(categoryToBeAdded);
        }
    }
}

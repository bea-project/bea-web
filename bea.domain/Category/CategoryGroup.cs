using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.Category
{
    public class CategoryGroup
    {
        public CategoryGroup()
        {
            this.Categories = new List<CategoryElement>();
        }

        public virtual int Id { get; set; }
        public virtual String Label { get; set; }
        public virtual IList<CategoryElement> Categories { get; set; }

        public virtual void AddCategory(CategoryElement category)
        {
            this.Categories.Add(category);
            category.CategoryGrp = this;
        }
    }
}

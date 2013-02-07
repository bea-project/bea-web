using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.Categories
{
    public class CategoryGroup
    {
        public CategoryGroup()
        {
            this.Categories = new List<Category>();
        }

        public virtual int Id { get; set; }
        public virtual String Label { get; set; }
        public virtual IList<Category> Categories { get; set; }

        public virtual void AddCategory(Category category)
        {
            this.Categories.Add(category);
            category.CategoryGroup = this;
        }
    }
}

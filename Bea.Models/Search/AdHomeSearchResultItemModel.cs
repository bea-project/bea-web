using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Categories;

namespace Bea.Models.Search
{
    public class AdHomeSearchResultItemModel
    {
        public int CategoryId { get; set; }
        public String CategoryLabel { get; set; }
        public String CategoryUrlPart { get; set; }
        public String CategoryImageName { get; set; }
        public int ResultCount { get; set; }

        public IList<AdHomeSearchResultItemModel> SubCategoriesResults { get; set; }

        public AdHomeSearchResultItemModel(Category category)
            : this()
        {
            CategoryId = category.Id;
            CategoryLabel = category.Label;
            CategoryUrlPart = category.LabelUrlPart;
            CategoryImageName = category.ImageName;
        }

        public AdHomeSearchResultItemModel()
        {
            SubCategoriesResults = new List<AdHomeSearchResultItemModel>();
        }
    }
}

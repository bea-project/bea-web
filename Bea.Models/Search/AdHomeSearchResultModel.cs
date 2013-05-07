using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Categories;

namespace Bea.Models.Search
{
    public class AdHomeSearchResultModel : AdSearchModel
    {
        public int SearchResultTotalCount { get; set; }
        public IList<AdHomeSearchResultItemModel> Results { get; set; }

        public AdHomeSearchResultModel(AdSearchModel searchModel)
            : base(searchModel)
        {
            Results = new List<AdHomeSearchResultItemModel>();
        }

        public AdHomeSearchResultModel()
        {
            Results = new List<AdHomeSearchResultItemModel>();
        }
    }
}

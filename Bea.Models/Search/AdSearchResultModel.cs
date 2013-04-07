using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models.Search;

namespace Bea.Models.Search
{
    public class AdSearchResultModel : AdvancedAdSearchModel
    {   
        public int SearchResultTotalCount { get; set; }
        public IList<AdSearchResultItemModel> SearchResult { get; set; }

        public AdSearchResultModel()
        {
            SearchResult = new List<AdSearchResultItemModel>();
        }

        public AdSearchResultModel(AdSearchModel searchModel)
            : base(searchModel)
        {
            SearchResult = new List<AdSearchResultItemModel>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models.Search;

namespace Bea.Models.Search
{
    public class AdSearchResultModel : AdSearchModel
    {   
        public int SearchResultTotalCount { get; set; }
        public IList<AdSearchResultItemModel> SearchResult { get; set; }

        public AdSearchResultModel()
        {
            SearchResult = new List<AdSearchResultItemModel>();
        }

        public AdSearchResultModel(AdSearchModel searchModel) : this()
        {
            this.SearchString = searchModel.SearchString;
            this.ProvinceSelectedId = searchModel.ProvinceSelectedId;
            this.CitySelectedId = searchModel.CitySelectedId;
            this.CategorySelectedId = searchModel.CategorySelectedId;
        }
    }
}

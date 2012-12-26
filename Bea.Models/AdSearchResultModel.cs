using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models
{
    public class AdSearchResultModel
    {
        public String SearchString { get; set; }
        public int? ProvinceSelectedId { get; set; }
        public int? CitySelectedId { get; set; }

        public int SearchResultTotalCount { get; set; }
        public IList<AdSearchResultItemModel> SearchResult { get; set; }

        public AdSearchResultModel()
        {
            SearchResult = new List<AdSearchResultItemModel>();
        }
    }
}

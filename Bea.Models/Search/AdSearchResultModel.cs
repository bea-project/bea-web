﻿using System;
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
            : this()
        {
            this.SearchString = searchModel.SearchString;
            this.CitySelectedId = searchModel.CitySelectedId;
            this.CategorySelectedId = searchModel.CategorySelectedId;
            this.CategorySelectedLabel = searchModel.CategorySelectedLabel;

            // TODO: add other properties !!
        }
    }
}

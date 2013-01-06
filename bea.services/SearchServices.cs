﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Core.Dal;
using Bea.Domain;
using Bea.Models;
using Bea.Domain.Ads;
using Bea.Domain.Search;

namespace Bea.Services
{
    public class SearchServices : ISearchServices
    {
        private IAdRepository _adRepository;

        public SearchServices(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public AdSearchResultModel SearchAds(AdSearchModel searchQuery)
        {
            String[] andSearchString = null;

            if (!String.IsNullOrEmpty(searchQuery.SearchString))
                andSearchString = searchQuery.SearchString.Trim().Split(' ');

            IList<SearchAdCache> searchResult = _adRepository.SearchAds(andSearchString, null, searchQuery.ProvinceSelectedId, searchQuery.CitySelectedId);
            
            AdSearchResultModel model = new AdSearchResultModel(searchQuery);
            model.SearchResultTotalCount = searchResult.Count;
            model.SearchResult = searchResult.Select(a => new AdSearchResultItemModel(a)).ToList();

            return model;
        }
    }
}

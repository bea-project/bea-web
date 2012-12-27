using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Core.Dal;
using Bea.Domain;
using Bea.Models;

namespace Bea.Services
{
    public class SearchServices : ISearchServices
    {
        private IAdRepository _adRepository;

        public SearchServices(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public AdSearchResultModel SearchAdsByTitle(String title)
        {
            IList<Ad> searchResult = _adRepository.SearchAdsByTitle(title);

            AdSearchResultModel model = new AdSearchResultModel();
            model.SearchString = title;
            model.SearchResultTotalCount = searchResult.Count;
            model.SearchResult = searchResult.Select(a => new AdSearchResultItemModel(a)).ToList();
            
            return model;
        }

        public AdSearchResultModel SearchAds(AdSearchModel searchQuery)
        {
            IList<Ad> searchResult = _adRepository.SearchAds(searchQuery.SearchString, searchQuery.ProvinceSelectedId, searchQuery.CitySelectedId);

            AdSearchResultModel model = new AdSearchResultModel(searchQuery);
            model.SearchResultTotalCount = searchResult.Count;
            model.SearchResult = searchResult.Select(a => new AdSearchResultItemModel(a)).ToList();

            return model;
        }
    }
}

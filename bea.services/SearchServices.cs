using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Core.Dal;
using Bea.Domain;
using Bea.Models;
using Bea.Domain.Ads;
using Bea.Domain.Search;
using Bea.Domain.Categories;
using Bea.Models.Search;

namespace Bea.Services
{
    public class SearchServices : ISearchServices
    {
        private IAdRepository _adRepository;
        private IRepository _repository;

        public SearchServices(IAdRepository adRepository, IRepository repository)
        {
            _adRepository = adRepository;
            _repository = repository;
        }

        public AdSearchResultModel SearchAds(AdSearchModel searchQuery)
        {
            String[] andSearchStrings = null;

            if (!String.IsNullOrEmpty(searchQuery.SearchString))
                andSearchStrings = searchQuery.SearchString.Trim().Split(' ');

            int[] categories = GetCategoryIdsFromQuery(searchQuery);

            
            Dictionary<String, String> searchParams = new Dictionary<String, String>();


            IList<SearchAdCache> searchResult = _adRepository.SearchAds(andSearchStrings, null, searchQuery.ProvinceSelectedId, searchQuery.CitySelectedId, categories);
            
            AdSearchResultModel model = new AdSearchResultModel(searchQuery);
            model.SearchResultTotalCount = searchResult.Count;
            model.SearchResult = searchResult.Select(a => new AdSearchResultItemModel(a)).ToList();

            return model;
        }

        private int[] GetCategoryIdsFromQuery(AdSearchModel searchQuery)
        {
            int[] categories = null;

            if (searchQuery.CategorySelectedId.HasValue)
            {
                if (searchQuery.CategorySelectedId.Value - CategoryServices.ID_MULTIPLIER > 0)
                {
                    categories = _repository.Get<CategoryGroup>(searchQuery.CategorySelectedId.Value - CategoryServices.ID_MULTIPLIER)
                        .Categories.ToList()
                        .Select(x => x.Id).ToArray();
                }
                else
                {
                    categories = new int[] { searchQuery.CategorySelectedId.Value };
                }
            }
            return categories;
        }
    }
}

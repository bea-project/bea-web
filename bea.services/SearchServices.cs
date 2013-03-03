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
        private readonly IAdRepository _adRepository;
        private readonly IRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILocationRepository _locationRepository;

        public SearchServices(IAdRepository adRepository, IRepository repository, ICategoryRepository categoryRepository, ILocationRepository locationRepository)
        {
            _adRepository = adRepository;
            _repository = repository;
            _categoryRepository = categoryRepository;
            _locationRepository = locationRepository;
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

            if (!searchQuery.CategorySelectedId.HasValue)
                return categories;

            Category selectedCategory = _repository.Get<Category>(searchQuery.CategorySelectedId);
            
            // If this is a prent category
            if (selectedCategory.SubCategories.Count != 0)
                categories = selectedCategory.SubCategories.Select(x => x.Id).ToArray();
            else
                categories = new int[] { searchQuery.CategorySelectedId.Value };

            return categories;
        }

        public AdSearchResultModel SearchAdsFromUrl(string cityLabel, string categoryLabel)
        {
            AdSearchModel model = new AdSearchModel();

            if (!String.IsNullOrEmpty(categoryLabel))
            {
                Category c = _categoryRepository.GetCategoryFromUrlPart(categoryLabel);
                model.CategorySelectedId = c != null ? c.Id : (int?) null;
            }

            return SearchAds(model);
        }
    }
}

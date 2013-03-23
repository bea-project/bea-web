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
using Bea.Models.Search.Vehicles;
using Bea.Tools;

namespace Bea.Services
{
    public class SearchServices : ISearchServices
    {
        private readonly IRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISearchRepository _searchRepository;
        private readonly IHelperService _helperService;
        private readonly IReferenceServices _referenceServices;


        public SearchServices(IRepository repository, ICategoryRepository categoryRepository, ISearchRepository searchRepository, IHelperService helperService, IReferenceServices referenceServices)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _searchRepository = searchRepository;
            _helperService = helperService;
            _referenceServices = referenceServices;
        }

        public AdSearchResultModel SearchAds(AdSearchModel searchQuery)
        {
            String[] andSearchStrings = null;

            if (!String.IsNullOrEmpty(searchQuery.SearchString))
                andSearchStrings = searchQuery.SearchString.Trim().Split(' ');

            int[] categories = GetCategoryIdsFromQuery(searchQuery);

            Dictionary<String, String> searchParams = new Dictionary<String, String>();

            IList<SearchAdCache> searchResult = _searchRepository.SearchAds(andSearchStrings, searchQuery.CitySelectedId, categories);

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

            // If this is a parent category
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
                if (c != null)
                {
                    model.CategorySelectedId = c.Id;
                    model.CategorySelectedLabel = c.Label;
                }
            }

            return SearchAds(model);
        }

        public AdSearchResultModel AdvancedSearchAds(AdSearchModel searchQuery)
        {
            Category selectedCategory = _repository.Get<Category>(searchQuery.CategorySelectedId);
            
            String[] andSearchStrings = null;

            if (!String.IsNullOrEmpty(searchQuery.SearchString))
                andSearchStrings = searchQuery.SearchString.Trim().Split(' ');

            int[] categories = GetCategoryIdsFromQuery(searchQuery);

            IList<SearchAdCache> searchResult = null;

            switch (selectedCategory.Type)
            {
                case AdTypeEnum.CarAd:
                    searchResult = SearchThroughVehicleAds<CarAd>(searchQuery as VehicleAdSearchModel, andSearchStrings, categories);
                    break;
                case AdTypeEnum.OtherVehiculeAd:
                    searchResult = SearchThroughVehicleAds<OtherVehicleAd>(searchQuery as VehicleAdSearchModel, andSearchStrings, categories);
                    break;
                case AdTypeEnum.MotoAd:
                    searchResult = SearchThroughVehicleAds<MotoAd>(searchQuery as VehicleAdSearchModel, andSearchStrings, categories);
                    break;

                case AdTypeEnum.SailingBoatAd:

                    break;

                case AdTypeEnum.MotorBoatAd:

                    break;

                case AdTypeEnum.MotorBoatEngineAd:

                    break;

                case AdTypeEnum.WaterSportAd:

                    break;

                case AdTypeEnum.RealEstateAd:

                    break;

                case AdTypeEnum.Ad:
                default:
                    searchResult = _searchRepository.SearchAds(andSearchStrings, searchQuery.CitySelectedId, categories);
                break;
            }

            // Create models for search results
            AdSearchResultModel model = new AdSearchResultModel(searchQuery);
            model.SearchResultTotalCount = searchResult.Count;
            model.SearchResult = searchResult.Select(a => new AdSearchResultItemModel(a)).ToList();
            
            return model;
        }

        public IList<SearchAdCache> SearchThroughVehicleAds<T>(VehicleAdSearchModel searchQuery, String[] andSearchStrings, int[] selectedCategoryIds) where T : VehicleAd
        {
            int? minYear = null, maxYear = null, minKm = null, maxKm = null;
            int? brandSelectedId = null;
            int? fuelSelectedId = null;
            Boolean? isAutomatic = null;
            int? minEngineSize = null, maxEngineSize = null;

            // First get all root restrictions for all vehicles
            if (searchQuery.AgeBracketSelectedId.HasValue)
            {
                maxYear = _referenceServices.GetAllAgeBrackets()[searchQuery.AgeBracketSelectedId.Value].LowValue;
                maxYear = _helperService.GetCurrentDateTime().Year - maxYear;
                minYear = _referenceServices.GetAllAgeBrackets()[searchQuery.AgeBracketSelectedId.Value].HighValue;
                minYear = _helperService.GetCurrentDateTime().Year - minYear;
            }

            if (searchQuery.KmBracketSelectedId.HasValue)
            {
                minKm = _referenceServices.GetAllKmBrackets()[searchQuery.KmBracketSelectedId.Value].LowValue;
                maxKm = _referenceServices.GetAllKmBrackets()[searchQuery.KmBracketSelectedId.Value].HighValue;
            }

            // Add custom car restrictions
            if (searchQuery is CarAdSearchModel)
            {
                CarAdSearchModel refinedSearchQuery = searchQuery as CarAdSearchModel;
                brandSelectedId = refinedSearchQuery.BrandSelectedId;
                fuelSelectedId = refinedSearchQuery.FuelSelectedId;
                isAutomatic = refinedSearchQuery.IsAutomatic;
            }

            // Add custom moto restrictions
            if (searchQuery is MotoAdSearchModel)
            {
                MotoAdSearchModel refinedSearchQuery = searchQuery as MotoAdSearchModel;
                brandSelectedId = refinedSearchQuery.BrandSelectedId;

                if (refinedSearchQuery.EngineSizeBracketSelectedId.HasValue)
                {
                    minEngineSize = _referenceServices.GetAllEngineSizeBrackets()[refinedSearchQuery.EngineSizeBracketSelectedId.Value].LowValue;
                    maxEngineSize = _referenceServices.GetAllEngineSizeBrackets()[refinedSearchQuery.EngineSizeBracketSelectedId.Value].HighValue;
                }
            }

            // Add custom other vehicles restrictions
            if (searchQuery is OtherVehicleAdSearchModel)
            {
                fuelSelectedId = (searchQuery as OtherVehicleAdSearchModel).FuelSelectedId;
            }

            return _searchRepository.SearchVehicleAds<T>(
                    andSearchStrings,
                    searchQuery.CitySelectedId,
                    selectedCategoryIds,
                    minKm, maxKm,
                    minYear, maxYear,
                    brandSelectedId,
                    fuelSelectedId,
                    isAutomatic,
                    minEngineSize, maxEngineSize);
        }
    }
}

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
using Bea.Tools;
using Bea.Domain.Ads.WaterSport;
using Bea.Domain.Location;

namespace Bea.Services
{
    public class SearchServices : ISearchServices
    {
        private readonly IRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISearchRepository _searchRepository;
        private readonly IHelperService _helperService;
        private readonly IReferenceServices _referenceServices;
        private readonly ILocationServices _locationServices;

        public SearchServices(IRepository repository, 
            ICategoryRepository categoryRepository, 
            ISearchRepository searchRepository, 
            IHelperService helperService, 
            IReferenceServices referenceServices,
            ILocationServices locationServices)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _searchRepository = searchRepository;
            _helperService = helperService;
            _referenceServices = referenceServices;
            _locationServices = locationServices;
        }

        public AdSearchResultModel SearchAds(AdSearchModel searchQuery)
        {
            // If this is a broad search, redirect to the base search through all ads
            if (!searchQuery.CategorySelectedId.HasValue)
                return LightSearchAds(searchQuery);

            Category selectedCategory = _repository.Get<Category>(searchQuery.CategorySelectedId);
            searchQuery.SetCategory(selectedCategory);

            // If this is a group category search, redirect to the base search through all this group ads
            if (selectedCategory.SubCategories.Count != 0)
                return LightSearchAds(searchQuery);

            return AdvancedSearchAds(searchQuery as AdvancedAdSearchModel);
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
                    model.CategoryImagePath = c.ImageName;
                }
            }

            if (!String.IsNullOrEmpty(cityLabel))
            {
                City city = _locationServices.GetCityFromLabelUrlPart(cityLabel);
                if (city != null)
                    model.CitySelectedId = city.Id;
            }

            return LightSearchAds(model);
        }

        internal AdSearchResultModel LightSearchAds(AdSearchModel searchQuery)
        {
            String[] andSearchStrings = null;

            if (!String.IsNullOrEmpty(searchQuery.SearchString))
                andSearchStrings = searchQuery.SearchString.Trim().Split(' ');

            int[] categories = GetCategoryIdsFromQuery(searchQuery.CategorySelectedId);

            Dictionary<String, String> searchParams = new Dictionary<String, String>();

            IList<SearchAdCache> searchResult = _searchRepository.SearchAds(andSearchStrings, searchQuery.CitySelectedId, categories);

            AdSearchResultModel model = new AdSearchResultModel(searchQuery);
            model.SearchResultTotalCount = searchResult.Count;
            model.SearchResult = searchResult.Select(a => new AdSearchResultItemModel(a)).ToList();

            return model;
        }
  
        internal AdSearchResultModel AdvancedSearchAds(AdvancedAdSearchModel searchQuery)
        {
            // This search relies on a selected category
            Category selectedCategory = _repository.Get<Category>(searchQuery.CategorySelectedId);

            // create search parameters object from AdvancedAdSearchModel
            AdSearchParameters searchParameters = CreateSearchParameters(searchQuery);

            // call repo with Type T based on switch on category with parameters (same for all objects)
            IList<SearchAdCache> searchResult = new List<SearchAdCache>();

            switch (selectedCategory.Type)
            {
                case AdTypeEnum.CarAd:
                    searchResult = _searchRepository.AdvancedSearchAds<CarAd>(searchParameters);
                    break;
                case AdTypeEnum.OtherVehiculeAd:
                    searchResult = _searchRepository.AdvancedSearchAds<OtherVehicleAd>(searchParameters);
                    break;
                case AdTypeEnum.MotoAd:
                    searchResult = _searchRepository.AdvancedSearchAds<MotoAd>(searchParameters);
                    break;
                case AdTypeEnum.RealEstateAd:
                    searchResult = _searchRepository.AdvancedSearchAds<RealEstateAd>(searchParameters);
                    break;
                case AdTypeEnum.MotorBoatAd:
                    searchResult = _searchRepository.AdvancedSearchAds<MotorBoatAd>(searchParameters);
                    break;
                case AdTypeEnum.SailingBoatAd:
                    searchResult = _searchRepository.AdvancedSearchAds<SailingBoatAd>(searchParameters);
                    break;
                case AdTypeEnum.MotorBoatEngineAd:
                    searchResult = _searchRepository.AdvancedSearchAds<MotorBoatEngineAd>(searchParameters);
                    break;
                case AdTypeEnum.WaterSportAd:
                    searchResult = _searchRepository.AdvancedSearchAds<WaterSportAd>(searchParameters);
                    break;
                case AdTypeEnum.Ad:
                    searchResult = _searchRepository.AdvancedSearchAds<Ad>(searchParameters);
                    break;
            }

            // Create models for search results
            AdSearchResultModel model = new AdSearchResultModel(searchQuery);
            model.SearchResultTotalCount = searchResult.Count;
            model.SearchResult = searchResult.Select(a => new AdSearchResultItemModel(a)).ToList();

            return model;
        }

        private int[] GetCategoryIdsFromQuery(int? categorySelectedId)
        {
            int[] categories = null;

            if (!categorySelectedId.HasValue)
                return categories;

            Category selectedCategory = _repository.Get<Category>(categorySelectedId);

            // If this is a parent category
            if (selectedCategory.SubCategories.Count != 0)
                categories = selectedCategory.SubCategories.Select(x => x.Id).ToArray();
            else
                categories = new int[] { categorySelectedId.Value };

            return categories;
        }

        private AdSearchParameters CreateSearchParameters(AdvancedAdSearchModel searchQuery)
        {
            AdSearchParameters parameters = new AdSearchParameters();

            if (!String.IsNullOrEmpty(searchQuery.SearchString))
                parameters.AndSearchStrings = searchQuery.SearchString.Trim().Split(' ');

            parameters.CategoryIds = GetCategoryIdsFromQuery(searchQuery.CategorySelectedId);
            parameters.CityId = searchQuery.CitySelectedId;

            parameters.MinPrice = searchQuery.MinPrice;
            parameters.MaxPrice = searchQuery.MaxPrice;

            // -- Vehicles specific properties -- //
            if (searchQuery.AgeBracketSelectedId.HasValue)
            {
                parameters.MaxYear = _referenceServices.GetAllAgeBrackets()[searchQuery.AgeBracketSelectedId.Value].LowValue;
                parameters.MaxYear = _helperService.GetCurrentDateTime().Year - parameters.MaxYear;
                parameters.MinYear = _referenceServices.GetAllAgeBrackets()[searchQuery.AgeBracketSelectedId.Value].HighValue;
                parameters.MinYear = _helperService.GetCurrentDateTime().Year - parameters.MinYear;
            }

            if (searchQuery.KmBracketSelectedId.HasValue)
            {
                parameters.MinKm = _referenceServices.GetAllKmBrackets()[searchQuery.KmBracketSelectedId.Value].LowValue;
                parameters.MaxKm = _referenceServices.GetAllKmBrackets()[searchQuery.KmBracketSelectedId.Value].HighValue;
            }

            if (searchQuery.EngineSizeBracketSelectedId.HasValue)
            {
                parameters.MinEngineSize = _referenceServices.GetAllEngineSizeBrackets()[searchQuery.EngineSizeBracketSelectedId.Value].LowValue;
                parameters.MaxEngineSize = _referenceServices.GetAllEngineSizeBrackets()[searchQuery.EngineSizeBracketSelectedId.Value].HighValue;
            }

            parameters.BrandId = searchQuery.BrandSelectedId;
            parameters.FueldId = searchQuery.FuelSelectedId;
            parameters.IsAuto = searchQuery.IsAutomatic;

            //-- Real Estate specific properties --//
            parameters.RealEstateTypeId = searchQuery.SelectedRealEstateTypeId;
            parameters.DistrictId = searchQuery.SelectedDistrictId;
            parameters.IsFurnished = searchQuery.IsFurnished;
            if (searchQuery.NbRoomsBracketSelectedId.HasValue)
            {
                parameters.MinNbRooms = _referenceServices.GetAllRealEstateNbRoomsBrackets()[searchQuery.NbRoomsBracketSelectedId.Value].LowValue;
                parameters.MaxNbRooms = _referenceServices.GetAllRealEstateNbRoomsBrackets()[searchQuery.NbRoomsBracketSelectedId.Value].HighValue;
            }
            if (searchQuery.SurfaceAreaBracketSelectedId.HasValue)
            {
                parameters.MinSurfaceArea = _referenceServices.GetAllSurfaceAreaBrackets()[searchQuery.SurfaceAreaBracketSelectedId.Value].LowValue;
                parameters.MaxSurfaceArea = _referenceServices.GetAllSurfaceAreaBrackets()[searchQuery.SurfaceAreaBracketSelectedId.Value].HighValue;
            }

            //-- Water sport specific properties --//
            parameters.MotorBoatTypeId = searchQuery.SelectedMotorBoatTypeId;
            parameters.MotorEngineTypeId = searchQuery.SelectedMotorTypeId;
            parameters.SailingBoatTypeId = searchQuery.SelectedSailingBoatTypeId;
            parameters.HullTypeId = searchQuery.SelectedHullTypeId;
            parameters.WaterTypeId = searchQuery.SelectedWaterTypeId;
            parameters.MinLength = searchQuery.MinLength;
            parameters.MaxLength = searchQuery.MaxLength;
            parameters.MinHp = searchQuery.MinHp;
            parameters.MaxHp = searchQuery.MaxHp;

            return parameters;
        }

        public AdHomeSearchResultModel QuickSearch(AdSearchModel searchQuery)
        {
            String[] andSearchStrings = null;

            if (!String.IsNullOrEmpty(searchQuery.SearchString))
                andSearchStrings = searchQuery.SearchString.Trim().Split(' ');

            IDictionary<Category, int> searchResult = _searchRepository.CountByCategory(andSearchStrings, searchQuery.CitySelectedId);

            AdHomeSearchResultModel result = new AdHomeSearchResultModel(searchQuery);
            IList<Category> processedParentCategories = new List<Category>();

            // Loop through all the parent categories
            foreach (KeyValuePair<Category, int> r in searchResult)
            {
                Category parentCategory = r.Key.ParentCategory;

                if (processedParentCategories.Contains(parentCategory))
                    continue;
                
                processedParentCategories.Add(parentCategory);

                AdHomeSearchResultItemModel m = new AdHomeSearchResultItemModel(parentCategory);
                result.Results.Add(m);

                foreach (Category c in parentCategory.SubCategories)
                {
                    if (!searchResult.ContainsKey(c))
                        continue;

                    AdHomeSearchResultItemModel r1 = new AdHomeSearchResultItemModel(c);
                    r1.ResultCount = searchResult[c];
                    m.ResultCount += r1.ResultCount;
                    m.SubCategoriesResults.Add(r1);
                }

                result.SearchResultTotalCount += m.ResultCount;
            }

            return result;
        }
    }
}

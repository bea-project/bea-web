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
using Bea.Domain.Categories;
using Bea.Models.Search;
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

            int[] categories = GetCategoryIdsFromQuery(searchQuery.CategorySelectedId);

            Dictionary<String, String> searchParams = new Dictionary<String, String>();

            IList<SearchAdCache> searchResult = _searchRepository.SearchAds(andSearchStrings, searchQuery.CitySelectedId, categories);

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

        public AdSearchResultModel SearchAdsFromUrl(string cityLabel, string categoryLabel)
        {
            AdvancedAdSearchModel model = new AdvancedAdSearchModel();

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

        public AdSearchResultModel AdvancedSearchAds(AdvancedAdSearchModel searchQuery)
        {
            // If this is a broad search, redirect to the base search through all ads
            if (!searchQuery.CategorySelectedId.HasValue)
                return SearchAds(searchQuery);

            // create search parameters object from AdvancedAdSearchModel
            AdSearchParameters searchParameters = CreateSearchParameters(searchQuery);

            Category selectedCategory = _repository.Get<Category>(searchQuery.CategorySelectedId);

            // call repo with Type T based on switch on category with parameters (same for all objects)
            IList<SearchAdCache> searchResult = null;

            switch(selectedCategory.Type)
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
                    
                //TODO: add the other types

                case AdTypeEnum.Ad:
                default:
                    searchResult = _searchRepository.AdvancedSearchAds<Ad>(searchParameters);
                    break;
            }

            // Create models for search results
            AdSearchResultModel model = new AdSearchResultModel(searchQuery);
            model.SearchResultTotalCount = searchResult.Count;
            model.SearchResult = searchResult.Select(a => new AdSearchResultItemModel(a)).ToList();

            return model;
        }

        public AdSearchParameters CreateSearchParameters(AdvancedAdSearchModel searchQuery)
        {
            AdSearchParameters parameters = new AdSearchParameters();

            if (!String.IsNullOrEmpty(searchQuery.SearchString))
                parameters.AndSearchStrings = searchQuery.SearchString.Trim().Split(' ');

            parameters.CategoryIds = GetCategoryIdsFromQuery(searchQuery.CategorySelectedId);
            parameters.CityId = searchQuery.CitySelectedId;

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
            parameters.MinNbRooms = searchQuery.MinNbRooms;
            parameters.MaxNbRooms = searchQuery.MaxNbRooms;

            return parameters;
        }
    }
}

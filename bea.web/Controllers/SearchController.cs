﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bea.Core.Services;
using Bea.Domain.Ads;
using Bea.Domain.Categories;
using Bea.Models.References;
using Bea.Models.Search;

namespace Bea.Web.Controllers
{
    public class SearchController : BaseController
    {
        private ISearchServices _searchServices;
        private ICategoryServices _categoryServices;
        private IEmailServices _emailService;

        public SearchController(ISearchServices searchServices, ICategoryServices categoryServices, ILocationServices locationServices, IReferenceServices referenceServices, IEmailServices emailService)
            : base(locationServices, referenceServices)
        {
            _searchServices = searchServices;
            _categoryServices = categoryServices;
            _emailService = emailService;
        }

        //
        // GET: /Search/
        public ActionResult Index(AdvancedAdSearchModel model)
        {
            ViewBag.Categories = _categoryServices.GetAllCategoriesOfAGroup(model.CategorySelectedId)
                .Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() });

            // Equivalent to Search/Search?
            if (model.CategorySelectedId.HasValue)
            {
                AdSearchResultModel result = _searchServices.SearchAds(model);
                return View(result);
            }
            // Equivalent to Search/QuickSearch? (when no categories)
            else
            {
                AdHomeSearchResultModel result = _searchServices.QuickSearch(model);
                return View("QuickSearch", result);
            }
        }

        //
        // GET: /Search/Search
        public ActionResult Search(AdvancedAdSearchModel model)
        {
            ViewBag.Categories = _categoryServices.GetAllCategoriesOfAGroup(model.CategorySelectedId)
                .Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() });

            AdSearchResultModel result = _searchServices.SearchAds(model);
            
            return View("Index", result);
        }

        //
        // GET: /Search/QuickSearch/
        public ActionResult QuickSearch(AdSearchModel model)
        {
            ViewBag.Categories = _categoryServices.GetAllCategoriesOfAGroup(model.CategorySelectedId)
                .Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() });

            AdHomeSearchResultModel result = _searchServices.QuickSearch(model);

            return View(result);
        }


        //
        // GET: /Search/SearchFromUrl
        public ActionResult SearchFromUrl(String cityLabel, String categoryLabel)
        {
            IList<CategoryItemModel> categoryGroup = _categoryServices.GetAllCategoriesOfAGroupFromUrlPart(categoryLabel);

            if (categoryGroup.Count == 0)
                return View("CategoryDoesNotExists");

            AdSearchResultModel result = _searchServices.SearchAdsFromUrl(cityLabel, categoryLabel);
            ViewBag.Categories = categoryGroup.Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() });

            return View("Index", result);
        }

        //
        // GET: /Search/AddParamters
        public PartialViewResult AddParamters(int categoryId)
        {
            Category selectedCategory = _categoryServices.GetCategoryById(categoryId);
            FillViewLists(selectedCategory);
            AdvancedAdSearchModel model = new AdvancedAdSearchModel();

            switch (selectedCategory.Type)
            {
                case AdTypeEnum.CarAd:
                    return PartialView("Shared/_CarAdSearch", model);

                case AdTypeEnum.MotoAd:
                    return PartialView("Shared/_MotoAdSearch", model);

                case AdTypeEnum.VehiculeAd:
                    return PartialView("Shared/_VehicleAdSearch", model);

                case AdTypeEnum.OtherVehiculeAd:
                    return PartialView("Shared/_OtherVehicleAdSearch", model);

                case AdTypeEnum.RealEstateAd:
                    return PartialView("Shared/_RealEstateAdSearch", model);

                case AdTypeEnum.MotorBoatAd:
                    return PartialView("Shared/_MotorBoatAdSearch", model);

                case AdTypeEnum.SailingBoatAd:
                    return PartialView("Shared/_SailingBoatAdSearch", model);

                case AdTypeEnum.MotorBoatEngineAd:
                    return PartialView("Shared/_MotorBoatEngineAdSearch", model);

                case AdTypeEnum.WaterSportAd:
                    return PartialView("Shared/_WaterSportAdSearch", model);
            }

            return null;
        }
    }
}

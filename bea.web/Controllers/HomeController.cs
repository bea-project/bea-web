using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Bea.Core.Services;
using Bea.Models;
using Bea.Domain.Categories;
using Bea.Domain.Ads;
using Bea.Models.Search;
using System.Threading.Tasks;

namespace Bea.Web.Controllers
{
    public class HomeController : BaseController
    {
        private ISearchServices _searchServices;
        private ICategoryServices _categoryServices;
        private IEmailServices _emailService;

        public HomeController(ISearchServices searchServices, ICategoryServices categoryServices, ILocationServices locationServices, IReferenceServices referenceServices, IEmailServices emailService)
            : base(locationServices, referenceServices)
        {
            _searchServices = searchServices;
            _categoryServices = categoryServices;
            _emailService = emailService;
        }

        public ActionResult Index()
        {
            return View(new AdSearchModel());
        }

        public ActionResult SearchFromUrl(String cityLabel, String categoryLabel)
        {
            AdSearchResultModel result = _searchServices.SearchAdsFromUrl(cityLabel, categoryLabel);
            return View("Search", result);
        }

        public ActionResult Search(AdSearchModel model)
        {
            AdSearchResultModel result = _searchServices.SearchAds(model);
            return View(result);
        }

        public ActionResult AdvancedSearch(AdvancedAdSearchModel model)
        {
            AdSearchResultModel result = _searchServices.AdvancedSearchAds(model);
            return View("Search", result);
        }

        public PartialViewResult AddParamters(int categoryId)
        {
            Category selectedCategory = _categoryServices.GetCategoryById(categoryId);
            FillViewLists(selectedCategory);
            AdvancedAdSearchModel model = new AdvancedAdSearchModel();

            switch (selectedCategory.Type)
            {
                case AdTypeEnum.CarAd:
                    return PartialView("Shared/Search/_CarAdSearch", model);

                case AdTypeEnum.MotoAd:
                    return PartialView("Shared/Search/_MotoAdSearch", model);

                case AdTypeEnum.VehiculeAd:
                    return PartialView("Shared/Search/_VehicleAdSearch", model);

                case AdTypeEnum.OtherVehiculeAd:
                    return PartialView("Shared/Search/_OtherVehicleAdSearch", model);

                case AdTypeEnum.RealEstateAd:
                    return PartialView("Shared/Search/_RealEstateAdSearch", model);

                case AdTypeEnum.MotorBoatAd:
                    return PartialView("Shared/Search/_MotorBoatAdSearch", model);

                case AdTypeEnum.SailingBoatAd:
                    return PartialView("Shared/Search/_SailingBoatAdSearch", model);

                case AdTypeEnum.MotorBoatEngineAd:
                    return PartialView("Shared/Search/_MotorBoatEngineAdSearch", model);

                case AdTypeEnum.WaterSportAd:
                    return PartialView("Shared/Search/_WaterSportAdSearch", model);
            }

            return null;
        }
    }
}

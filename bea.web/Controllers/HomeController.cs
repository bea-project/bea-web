using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Bea.Core.Services;
using Bea.Models;
using Bea.Domain.Categories;
using Bea.Domain.Ads;
using Bea.Models.Create.Vehicules;
using Bea.Models.Create.WaterSport;
using Bea.Models.Create.RealEstate;
using Bea.Models.Search;
using Bea.Models.Search.Vehicles;
using Bea.Models.Search.WaterSport;
using Bea.Models.Search.RealEstate;
using System.Threading.Tasks;

namespace Bea.Web.Controllers
{
    public class HomeController : BaseController
    {
        private ISearchServices _searchServices;
        private ICategoryServices _categoryServices;
        private IEmailServices _emailService;

        public HomeController(ISearchServices searchServices, ICategoryServices categoryServices, ILocationServices locationServices, IReferenceServices referenceServices, IEmailServices emailService)
            :base(locationServices,referenceServices)
        {
            if (searchServices == null)
                throw new ArgumentNullException("searchServices");

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

                case AdTypeEnum.MotorBoatAd:
                    ViewBag.Hps = base._referenceServices.GetAllHps().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    ViewBag.Lengths = base._referenceServices.GetAllMotorBoatLength().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    MotorBoatAdSearchModel motorBoatModel = new MotorBoatAdSearchModel();
                    return PartialView("Shared/Search/_MotorBoatAdSearch", motorBoatModel);

                case AdTypeEnum.SailingBoatAd:
                    ViewBag.Lengths = base._referenceServices.GetAllSailingBoatLength().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    SailingBoatAdSearchModel sailingBoatModel = new SailingBoatAdSearchModel();
                    return PartialView("Shared/Search/_SailingBoatAdSearch", sailingBoatModel);

                case AdTypeEnum.MotorBoatEngineAd:
                    ViewBag.Hps = base._referenceServices.GetAllHps().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    MotorBoatEngineAdSearchModel motorBoatEngineModel = new MotorBoatEngineAdSearchModel();
                    return PartialView("Shared/Search/_MotorBoatEngineAdSearch", motorBoatEngineModel);

                case AdTypeEnum.WaterSportAd:
                    WaterSportAdSearchModel waterSportModel = new WaterSportAdSearchModel();
                    return PartialView("Shared/Search/_WaterSportAdSearch", waterSportModel);

                case AdTypeEnum.RealEstateAd:
                    ViewBag.Rooms = base._referenceServices.GetAllRealEstateNbRoom().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    RealEstateAdSearchModel realEstateModel = new RealEstateAdSearchModel();
                    return PartialView("Shared/Search/_RealEstateAdSearch", realEstateModel);
            }
            return null;
        }
    }
}

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
        private IEmailService _emailService;

        public HomeController(ISearchServices searchServices, ICategoryServices categoryServices, ILocationServices locationServices, IReferenceServices referenceServices, IEmailService emailService)
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
            AdSearchResultModel result = _searchServices.SearchAds(new AdSearchResultModel());
            return View(result);
        }

        public ActionResult Search(AdSearchModel model)
        {
            AdSearchResultModel result = _searchServices.SearchAds(model);
            return View("Index", result);
        }

        public PartialViewResult AddParamters(int categoryId)
        {
            Category selectedCategory = _categoryServices.GetCategoryById(categoryId);
            FillViewLists(selectedCategory);
            switch (selectedCategory.Type)
            {
                case AdTypeEnum.CarAd:
                    ViewBag.Kms = base._referenceServices.GetAllKms().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    AdCarSearchModel carModel = new AdCarSearchModel();
                    return PartialView("Shared/Search/_CarAdSearch", carModel);

                case AdTypeEnum.MotoAd:
                    ViewBag.Kms = base._referenceServices.GetAllKms().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    ViewBag.Engines = base._referenceServices.GetAllEngineSizes().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    AdMotoSearchModel motoModel = new AdMotoSearchModel();
                    return PartialView("Shared/Search/_MotoAdSearch", motoModel);

                case AdTypeEnum.VehiculeAd:
                    ViewBag.Kms = base._referenceServices.GetAllKms().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    AdVehicleSearchModel vehicleModel = new AdVehicleSearchModel();
                    return PartialView("Shared/Search/_VehicleAdSearch", vehicleModel);

                case AdTypeEnum.OtherVehiculeAd:
                    ViewBag.Kms = base._referenceServices.GetAllKms().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    AdOtherVehicleSearchModel otherVehicleModel = new AdOtherVehicleSearchModel();
                    return PartialView("Shared/Search/_OtherVehicleAdSearch", otherVehicleModel);

                case AdTypeEnum.MotorBoatAd:
                    ViewBag.Hps = base._referenceServices.GetAllHps().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    ViewBag.Lengths = base._referenceServices.GetAllMotorBoatLength().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    AdMotorBoatSearchModel motorBoatModel = new AdMotorBoatSearchModel();
                    return PartialView("Shared/Search/_MotorBoatAdSearch", motorBoatModel);

                case AdTypeEnum.SailingBoatAd:
                    ViewBag.Lengths = base._referenceServices.GetAllSailingBoatLength().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    AdSailingBoatSearchModel sailingBoatModel = new AdSailingBoatSearchModel();
                    return PartialView("Shared/Search/_SailingBoatAdSearch", sailingBoatModel);

                case AdTypeEnum.MotorBoatEngineAd:
                    ViewBag.Hps = base._referenceServices.GetAllHps().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    AdMotorBoatEngineSearchModel motorBoatEngineModel = new AdMotorBoatEngineSearchModel();
                    return PartialView("Shared/Search/_MotorBoatEngineAdSearch", motorBoatEngineModel);

                case AdTypeEnum.WaterSportAd:
                    AdWaterSportSearchModel waterSportModel = new AdWaterSportSearchModel();
                    return PartialView("Shared/Search/_WaterSportAdSearch", waterSportModel);

                case AdTypeEnum.RealEstateAd:
                    ViewBag.Rooms = base._referenceServices.GetAllRealEstateNbRoom().Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    AdRealEstateSearchModel realEstateModel = new AdRealEstateSearchModel();
                    return PartialView("Shared/Search/_realEstateAdSearch", realEstateModel);
            }
            return null;
        }

    }
}

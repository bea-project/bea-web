using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bea.Core.Services;
using Bea.Domain.Categories;
using Bea.Domain.Ads;
using Bea.Models.Create;
using Bea.Models.Create.Vehicules;
using Bea.Models.Create.WaterSport;
using Bea.Domain.Ads.WaterSport;
using Bea.Models.Create.RealEstate;
using Bea.Models;

namespace Bea.Web.Controllers
{
    public class PostController : Controller
    {
        private IAdServices _adServices;
        private ILocationServices _locationServices;
        private IUserServices _userServices;
        private ICategoryServices _categoryServices;
        private IAdDataConsistencyServices _adConsistencyServices;
        private IReferenceServices _referenceServices;
        private IAdActivationServices _adActivationServices;

        public PostController(IAdServices adServices, ILocationServices locationServices, IUserServices userServices, ICategoryServices categoryServices, IAdDataConsistencyServices adConsistencyServices, IReferenceServices referenceServices, IAdActivationServices adActivationServices)
        {
            _adServices = adServices;
            _locationServices = locationServices;
            _userServices = userServices;
            _categoryServices = categoryServices;
            _adConsistencyServices = adConsistencyServices;
            _referenceServices = referenceServices;
            _adActivationServices = adActivationServices;
        }

        //
        // GET: /Post/
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Post/Details/{id}
        public ActionResult Details(long id)
        {
            var result = _adServices.GetAdDetails(id);

            if (result == null)
                return HttpNotFound("Cette annonce n'existe pas ou est désactivée");

            return View(result);
        }

        //
        // GET: /Post/Activate/{id}/{activationToken}
        public ActionResult Activate(long id, string activationToken)
        {
            var result = _adActivationServices.ActivateAd(id, activationToken);

            if (result == null)
                return HttpNotFound("Cette annonce n'existe pas ou est désactivée");

            return View(result);
        }

        public ActionResult Delete(long id)
        {
            _adServices.DeleteAdById(id);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            AdCreateModel model = new AdCreateModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AdCreateModel model, FormCollection collection)
        {
            Dictionary<string, string> form = collection.AllKeys.ToDictionary(k => k, v => collection[v]);
            BaseAd newAd = _adServices.GetAdFromModel(model, form);
            IDictionary<string, string> errors = _adConsistencyServices.GetAdDataConsistencyErrors(newAd);
            foreach (string key in errors.Keys)
                ModelState.AddModelError(key, errors[key]);
            if (ModelState.IsValid)
            {
                newAd.IsActivated = false;
                _adServices.AddAd(newAd);
                _adActivationServices.SendActivationEmail(newAd);
                return RedirectToAction("Index", "Home");
            }
            AdCreateModel returnModel = GetModelFromBaseAd(newAd, model);
            return View(returnModel);
        }

        //
        // GET: /Post/AddParamters/{id}
        public PartialViewResult AddParamters(int categoryId)
        {
            Category selectedCategory = _categoryServices.GetCategoryById(categoryId);
            FillViewLists(selectedCategory);
            switch (selectedCategory.Type)
            {
                case AdTypeEnum.CarAd:
                    AdCarCreateModel carModel = new AdCarCreateModel();
                    return PartialView("Shared/Create/_CarAdCreate", carModel);

                case AdTypeEnum.MotoAd:
                    AdMotoCreateModel motoModel = new AdMotoCreateModel();
                    return PartialView("Shared/Create/_MotoAdCreate", motoModel);

                case AdTypeEnum.VehiculeAd:
                    AdVehicleCreateModel vehicleModel = new AdVehicleCreateModel();
                    return PartialView("Shared/Create/_VehicleAdCreate", vehicleModel);

                case AdTypeEnum.OtherVehiculeAd:
                    AdOtherVehicleCreateModel otherVehicleModel = new AdOtherVehicleCreateModel();
                    return PartialView("Shared/Create/_OtherVehicleAdCreate", otherVehicleModel);

                case AdTypeEnum.MotorBoatAd:
                    AdMotorBoatCreateModel motorBoatModel = new AdMotorBoatCreateModel();
                    return PartialView("Shared/Create/_MotorBoatAdCreate", motorBoatModel);

                case AdTypeEnum.SailingBoatAd:
                    AdSailingBoatCreateModel sailingBoatModel = new AdSailingBoatCreateModel();
                    return PartialView("Shared/Create/_SailingBoatAdCreate", sailingBoatModel);

                case AdTypeEnum.MotorBoatEngineAd:
                    AdMotorBoatEngineCreateModel motorBoatEngineModel = new AdMotorBoatEngineCreateModel();
                    return PartialView("Shared/Create/_MotorBoatEngineAdCreate", motorBoatEngineModel);

                case AdTypeEnum.WaterSportAd:
                    AdWaterSportCreateModel waterSportModel = new AdWaterSportCreateModel();
                    return PartialView("Shared/Create/_WaterSportAdCreate", waterSportModel);

                case AdTypeEnum.RealEstateAd:
                    AdRealEstateCreateModel realEstateModel = new AdRealEstateCreateModel();
                    return PartialView("Shared/Create/_realEstateAdCreate", realEstateModel);
            }
            return null;
        }

        private AdCreateModel GetModelFromBaseAd(BaseAd ad, AdCreateModel createModel)
        {   
            if (ad.Category == null)
                return createModel;
         
            FillViewLists(ad.Category);
            
            switch (ad.Category.Type)
            {
                case AdTypeEnum.CarAd:
                    AdCarCreateModel adCarCreateModel = new AdCarCreateModel(ad as CarAd);
                    return adCarCreateModel;

                case AdTypeEnum.MotoAd:
                    AdMotoCreateModel adMotoCreateModel = new AdMotoCreateModel(ad as MotoAd);
                    return adMotoCreateModel;

                case AdTypeEnum.OtherVehiculeAd:
                    AdOtherVehicleCreateModel adOtherVehicleCreateModel = new AdOtherVehicleCreateModel(ad as OtherVehicleAd);
                    return adOtherVehicleCreateModel;

                case AdTypeEnum.VehiculeAd:
                    AdVehicleCreateModel adVehicleCreateModel = new AdVehicleCreateModel(ad as VehicleAd);
                    return adVehicleCreateModel;

                case AdTypeEnum.MotorBoatAd:
                    AdMotorBoatCreateModel adMotorBoatCreateModel = new AdMotorBoatCreateModel(ad as MotorBoatAd);
                    return adMotorBoatCreateModel;

                case AdTypeEnum.SailingBoatAd:
                    AdSailingBoatCreateModel adSailingBoatCreateModel = new AdSailingBoatCreateModel(ad as SailingBoatAd);
                    return adSailingBoatCreateModel;

                case AdTypeEnum.MotorBoatEngineAd:
                    AdMotorBoatEngineCreateModel adMotorBoatEngineCreateModel = new AdMotorBoatEngineCreateModel(ad as MotorBoatEngineAd);
                    return adMotorBoatEngineCreateModel;

                case AdTypeEnum.WaterSportAd:
                    AdWaterSportCreateModel adWaterSportCreateModel = new AdWaterSportCreateModel(ad as WaterSportAd);
                    return adWaterSportCreateModel;

                case AdTypeEnum.RealEstateAd:
                    AdRealEstateCreateModel adRealEstateCreateModel = new AdRealEstateCreateModel(ad as RealEstateAd);
                    return adRealEstateCreateModel;

                case AdTypeEnum.Ad:
                    return createModel;
            }
            return null;
        }

        public void FillViewLists(Category category)
        {
            if (category == null)
                return;
            switch (category.Type)
            {
                case AdTypeEnum.CarAd:
                    ViewBag.Years = _referenceServices.GetAllYears(40).Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    ViewBag.Brands = _referenceServices.GetAllCarBrands().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    ViewBag.Fuels = _referenceServices.GetAllCarFuels().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
                case AdTypeEnum.MotoAd:
                    ViewBag.Brands = _referenceServices.GetAllMotoBrands().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    ViewBag.Years = _referenceServices.GetAllYears(40).Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    break;
                case AdTypeEnum.OtherVehiculeAd:
                    ViewBag.Years = _referenceServices.GetAllYears(40).Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    ViewBag.Fuels = _referenceServices.GetAllCarFuels().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
                case AdTypeEnum.VehiculeAd:
                    ViewBag.Years = _referenceServices.GetAllYears(40).Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    break;
                case AdTypeEnum.MotorBoatAd:
                    ViewBag.Years = _referenceServices.GetAllYears(40).Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    ViewBag.Types = _referenceServices.GetAllMotorBoatTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    ViewBag.MotorTypes = _referenceServices.GetAllMotorBoatEngineTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
                case AdTypeEnum.SailingBoatAd:
                    ViewBag.Years = _referenceServices.GetAllYears(40).Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    ViewBag.Types = _referenceServices.GetAllSailingBoatTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    ViewBag.HullTypes = _referenceServices.GetAllSailingBoatHullTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
                case AdTypeEnum.MotorBoatEngineAd:
                    ViewBag.Years = _referenceServices.GetAllYears(40).Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    ViewBag.Types = _referenceServices.GetAllMotorBoatEngineTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
                case AdTypeEnum.WaterSportAd:
                    ViewBag.Types = _referenceServices.GetAllWaterSportTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
                case AdTypeEnum.RealEstateAd:
                    ViewBag.Types = _referenceServices.GetAllRealEstateTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    ViewBag.Districts = _locationServices.GetAllDistricts().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
            }
        }

        public ActionResult Contact(long id)
        {
            var result = _adServices.GetAdById(id);

            if (result == null)
                return HttpNotFound("Cette annonce n'existe pas ou est désactivée");

            ContactUserFormModel contactUserFormModel = new ContactUserFormModel(result.Title, result.CreatedBy.Firstname, result.CreatedBy.UserId, result.Id);
            return View(contactUserFormModel);
        }

        [HttpPost]
        public ActionResult Contact(ContactUserFormModel model)
        {
            IDictionary<string, string> errors = _adConsistencyServices.GetDataConsistencyErrors(model);
            foreach (string key in errors.Keys)
                ModelState.AddModelError(key, errors[key]);
            if (ModelState.IsValid)
            {
                var user = _userServices.GetUserFromId(model.UserId);
                model.EmailTo = user.Email;
                _adActivationServices.SendEmailToUser(model);
                if (model.CopySender)
                {
                    model.EmailTo = model.Email;
                    _adActivationServices.SendEmailToUser(model);
                }
                AdMessageModel messageModel = new AdMessageModel();
                messageModel.AdId = model.AdId;
                messageModel.InfoMessage = "Votre email a été envoyé !";
                return View("MessageSent", messageModel);
            }
            return View(model);
        }
    }
}
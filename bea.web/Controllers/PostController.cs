using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bea.Core.Services;
using Bea.Core.Services.Ads;
using Bea.Domain.Ads;
using Bea.Domain.Ads.WaterSport;
using Bea.Domain.Categories;
using Bea.Models.Create;
using Bea.Models;
using Bea.Models.Delete;
using Bea.Models.Request;
using System;

namespace Bea.Web.Controllers
{
    public class PostController : Controller
    {
        private IAdServices _adServices;
        private IAdDetailsServices _adDetailsServices;
        private ILocationServices _locationServices;
        private IUserServices _userServices;
        private ICategoryServices _categoryServices;
        private IAdDataConsistencyServices _adConsistencyServices;
        private IReferenceServices _referenceServices;
        private IAdActivationServices _adActivationServices;
        private IAdDeletionServices _adDeletionServices;
        private IAdRequestServices _adRequestServices;

        public PostController(IAdServices adServices, IAdDetailsServices adDetailsServices, ILocationServices locationServices, IUserServices userServices, ICategoryServices categoryServices, IAdDataConsistencyServices adConsistencyServices, IReferenceServices referenceServices, IAdActivationServices adActivationServices, IAdDeletionServices adDeletionServices, IAdRequestServices adRequestServices)
        {
            _adServices = adServices;
            _adDetailsServices = adDetailsServices;
            _locationServices = locationServices;
            _userServices = userServices;
            _categoryServices = categoryServices;
            _adConsistencyServices = adConsistencyServices;
            _referenceServices = referenceServices;
            _adActivationServices = adActivationServices;
            _adDeletionServices = adDeletionServices;
            _adRequestServices = adRequestServices;
        }

        //
        // GET: /Post/
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }



        #region Consult

        //
        // GET: /Post/Details/{id}
        public ActionResult Details(long id)
        {
            var result = _adDetailsServices.GetAdDetails(id);

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

        #endregion

        #region Delete

        //
        // GET: /Post/Delete/{id}
        public ActionResult Delete(long id)
        {
            var result = _adDeletionServices.DeleteAd(id);
            return View(result);
        }

        //
        // POST: /Post/Delete/{DeleteAdModel}
        [HttpPost]
        public ActionResult Delete(DeleteAdModel model)
        {
            var result = _adDeletionServices.DeleteAd(model);
            if (result.NbTry > 0)
                ModelState.AddModelError("Password", "Mot de passe incorrect");
            
            return View(result);
        }

        #endregion

        #region Request

        public ActionResult Request()
        {
            AdRequestModel model = new AdRequestModel();
            model.CanRequestAd = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Request(AdRequestModel model)
        {
            AdRequestModel result = new AdRequestModel();
            if (!_adConsistencyServices.IsEmailValid(model.Email))
            {
                result.CanRequestAd = true;
                result.Email = model.Email;
                ModelState.AddModelError("Email", "Email incorrect");
                return View(result);
            }
            result = _adRequestServices.RequestAds(model);
            return View(result);
        }

        #endregion

        #region Create

        public ActionResult Create()
        {
            AdvancedAdCreateModel model = new AdvancedAdCreateModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdvancedAdCreateModel model)
        {
            IDictionary<string, string> errors = _adConsistencyServices.GetAdDataConsistencyErrors(model);

            foreach (string key in errors.Keys)
                ModelState.AddModelError(key, errors[key]);

            if (ModelState.IsValid)
            {
                BaseAd newAd = _adServices.GetAdFromAdCreateModel(model);
                newAd.IsActivated = false;
                _adServices.AddAd(newAd);
                return View("Created");
            }
            FillViewLists(model.Type);
            return View(model);
        }

        public PartialViewResult AddParamters(int categoryId)
        {
            Category selectedCategory = _categoryServices.GetCategoryById(categoryId);
            FillViewLists(selectedCategory.Type);
            AdvancedAdCreateModel model = new AdvancedAdCreateModel();
            switch (selectedCategory.Type)
            {
                case AdTypeEnum.CarAd:
                    model.Type = AdTypeEnum.CarAd;
                    return PartialView("Shared/Create/_CarAdCreate", model);

                case AdTypeEnum.MotoAd:
                    model.Type = AdTypeEnum.MotoAd;
                    return PartialView("Shared/Create/_MotoAdCreate", model);

                case AdTypeEnum.VehiculeAd:
                    model.Type = AdTypeEnum.VehiculeAd;
                    return PartialView("Shared/Create/_VehicleAdCreate", model);

                case AdTypeEnum.OtherVehiculeAd:
                    model.Type = AdTypeEnum.OtherVehiculeAd;
                    return PartialView("Shared/Create/_OtherVehicleAdCreate", model);

                case AdTypeEnum.MotorBoatAd:
                    model.Type = AdTypeEnum.MotorBoatAd;
                    return PartialView("Shared/Create/_MotorBoatAdCreate", model);

                case AdTypeEnum.SailingBoatAd:
                    model.Type = AdTypeEnum.SailingBoatAd;
                    return PartialView("Shared/Create/_SailingBoatAdCreate", model);

                case AdTypeEnum.MotorBoatEngineAd:
                    model.Type = AdTypeEnum.MotorBoatEngineAd;
                    return PartialView("Shared/Create/_MotorBoatEngineAdCreate", model);

                case AdTypeEnum.WaterSportAd:
                    model.Type = AdTypeEnum.WaterSportAd;
                    return PartialView("Shared/Create/_WaterSportAdCreate", model);

                case AdTypeEnum.RealEstateAd:
                    model.Type = AdTypeEnum.RealEstateAd;
                    return PartialView("Shared/Create/_realEstateAdCreate", model);
            }
            return null;
        }

        public void FillViewLists(AdTypeEnum type)
        {
            switch (type)
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
        #endregion
        public ActionResult Contact(long id)
        {
            var result = _adServices.GetAdById(id);

            if (result == null)
                return HttpNotFound("Cette annonce n'existe pas ou est désactivée");

            ContactUserFormModel contactUserFormModel = new ContactUserFormModel(result.Title, result.CreatedBy.Firstname, result.CreatedBy.Id, result.Id);
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
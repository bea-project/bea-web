using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bea.Core.Services;
using Bea.Domain;
using Bea.Models;
using System.Diagnostics;
using Bea.Domain.Location;
using Bea.Domain.Categories;
using Bea.Domain.Ads;
using Bea.Domain.Search;
using Bea.Models.Create;
using Bea.Models.Create.Vehicules;

namespace Bea.Web.Controllers
{
    public class AdController : Controller
    {
        private IAdServices _adServices;
        private ILocationServices _locationServices;
        private IUserServices _userServices;
        private ICategoryServices _categoryServices;
        private IAdDataConsistencyServices _adConsistencyServices;
        private IReferenceServices _referenceServices;


        public AdController(IAdServices adServices, ILocationServices locationServices, IUserServices userServices, ICategoryServices categoryServices, IAdDataConsistencyServices adConsistencyServices, IReferenceServices referenceServices)
        {
            _adServices = adServices;
            _locationServices = locationServices;
            _userServices = userServices;
            _categoryServices = categoryServices;
            _adConsistencyServices = adConsistencyServices;
            _referenceServices = referenceServices;
        }

        //
        // GET: /Ad/
        public ActionResult Index()
        {
            var result = _adServices.GetAllAds();
            return View(result);
        }

        //
        // GET: /Ad/Details/{id}
        public ActionResult Details(long id)
        {
            var result = _adServices.GetAdDetails(id);

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
                newAd.IsValidated = false;
                _adServices.AddAd(newAd);
                return RedirectToAction("Index", "Home");
            }
            AdCreateModel returnModel = GetModelFromBaseAd(newAd, model);
            return View(returnModel);
        }

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
                
                case AdTypeEnum.OtherVehiculeAd:
                    AdOtherVehicleCreateModel otherVehicleModel = new AdOtherVehicleCreateModel();
                    return PartialView("Shared/Create/_OtherVehicleAdCreate", otherVehicleModel);
            }
            return null;
        }

        private AdCreateModel GetModelFromBaseAd(BaseAd ad, AdCreateModel createModel)
        {
            AdCreateModel model = null;
            FillViewLists(ad.Category);
            switch (ad.Category.Type)
            {
                case AdTypeEnum.CarAd:
                    AdCarCreateModel adCarCreateModel = new AdCarCreateModel(ad as CarAd, createModel);
                    return adCarCreateModel;

                case AdTypeEnum.MotoAd:
                    AdMotoCreateModel adMotoCreateModel = new AdMotoCreateModel(ad as MotoAd, createModel);
                    return adMotoCreateModel;

                case AdTypeEnum.OtherVehiculeAd:
                    AdOtherVehicleCreateModel adOtherVehicleCreateModel = new AdOtherVehicleCreateModel(ad as OtherVehicleAd, createModel);
                    return adOtherVehicleCreateModel;

                case AdTypeEnum.Ad:
                    return createModel;
            }
            return model;
        }


        private void FillViewLists(Category category)
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
                    ViewBag.Years = _referenceServices.GetAllYears(40).Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    break;
                case AdTypeEnum.OtherVehiculeAd:
                    ViewBag.Years = _referenceServices.GetAllYears(40).Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                    ViewBag.Fuels = _referenceServices.GetAllCarFuels().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
            }
        }
    }
}
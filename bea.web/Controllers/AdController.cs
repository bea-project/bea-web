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
                return HttpNotFound();

            return View(result);
        }

        public ActionResult Delete(long id)
        {
            _adServices.DeleteAdById(id);
            return RedirectToAction("Index","Home");
        }

        public ActionResult Create()
        {
            AdCreateModel model = new AdCreateModel();
            
            model.Provinces = _locationServices.GetAllProvinces().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
            model.Categories = _categoryServices.GetAllCategoryGroupsWithCategories().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AdCreateModel model, FormCollection collection)
        {

            Dictionary<string, string> form = collection.AllKeys.ToDictionary(k => k, v => collection[v]);
            BaseAd newAd = _adServices.GetAdFromModel(model,form);
            Dictionary<string, string> errors = _adConsistencyServices.GetAdDataConsistencyErrors(newAd, model.SelectedProvinceId);
            foreach (string key in errors.Keys)
                ModelState.AddModelError(key, errors[key]);
            if (ModelState.IsValid)
            {
                _adServices.AddAd(newAd);
                //SearchAdCache cacheAd = new SearchAdCache(newAd);

                return RedirectToAction("Index", "Home");
            }

            AdCreateModel returnModel = GetModelFromBaseAd(newAd,model);


            returnModel.Provinces = _locationServices.GetAllProvinces().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
            if (returnModel.SelectedProvinceId != null)
                returnModel.Cities = _locationServices.GetCitiesFromProvince(model.SelectedProvinceId.Value).Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
            returnModel.Categories = _categoryServices.GetAllCategoryGroupsWithCategories().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
            return View(returnModel);
        }

        public PartialViewResult AddParamters(int categoryId)
        {
            Category selectedCategory = _categoryServices.GetCategoryById(categoryId);
            if (selectedCategory.Label.Equals("Voitures"))
            {
                AdCarCreateModel adCarCreateModel = new AdCarCreateModel();
                adCarCreateModel.FuelList = _referenceServices.GetAllCarFuels().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                adCarCreateModel.BrandsList = _referenceServices.GetAllCarBrands().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                return PartialView("Shared/_CarAdCreate",adCarCreateModel);
            }
            return null;
        }

        private AdCreateModel GetModelFromBaseAd(BaseAd ad, AdCreateModel createModel)
        {
            AdCreateModel model = null;
            switch (ad.AdType)
            {
                case AdTypeEnum.CarAd:
                    AdCarCreateModel adCarCreateModel = new AdCarCreateModel(ad as CarAd, createModel);
                    adCarCreateModel.FuelList = _referenceServices.GetAllCarFuels().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    adCarCreateModel.BrandsList = _referenceServices.GetAllCarBrands().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    model = adCarCreateModel;
                    break; 

                case AdTypeEnum.Ad:
                    return createModel;
            }
            return model;
        }


    }
}
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

namespace Bea.Web.Controllers
{
    public class AdController : Controller
    {
        private IAdServices _adServices;
        private ILocationServices _locationServices;
        private IUserServices _userServices;
        private ICategoryServices _categoryServices;

        public AdController(IAdServices adServices, ILocationServices locationServices, IUserServices userServices, ICategoryServices categoryServices)
        {
            _adServices = adServices;
            _locationServices = locationServices;
            _userServices = userServices;
            _categoryServices = categoryServices;
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
        public ActionResult Create(AdCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Ad newAd = AdCreateModelToAd(model);
                _adServices.AddAd(newAd);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public Ad AdCreateModelToAd(AdCreateModel model)
        {
            Ad ad = new Ad();
            User user = _userServices.GetUserFromEmail(model.Email);
            City city = _locationServices.GetCityFromId(model.SelectedCityId);
            Category category = _categoryServices.GetCategoryById(model.SelectedCategoryId);
            ad.CreatedBy = user;
            ad.Body = model.Body;
            ad.City = city;
            ad.CreationDate = DateTime.Now;
            ad.Price = model.Price.GetValueOrDefault();
            ad.Title = model.Title;
            ad.IsOffer = model.IsOffer;
            ad.Category = category;
            return ad;
        }


    }
}
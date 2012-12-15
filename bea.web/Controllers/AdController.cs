using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bea.Core.Services;
using Bea.Domain;

namespace Bea.Web.Controllers
{
    public class AdController : Controller
    {
        private IAdServices _adServices;

        public AdController(IAdServices adServices)
        {
            if (adServices == null)
                throw new ArgumentNullException("adServices");

            _adServices = adServices;
        }

        //
        // GET: /Ad/
        public ActionResult Index_Test()
        {
            var result = _adServices.CountAdsByCities();
            return View(result);
        }

        //
        // GET: /Ad/
        public ActionResult Index()
        {
            var result = _adServices.GetAllAds();
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Ad ad)
        {
            return View(ad);
        }

        //
        // GET: /Ad/Details/{id}
        public ActionResult Details(long id)
        {
            return View();
        }

    }
}

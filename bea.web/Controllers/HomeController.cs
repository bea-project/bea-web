using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Bea.Core.Services;
using Bea.Models;

namespace Bea.Web.Controllers
{
    public class HomeController : Controller
    {
        private ISearchServices _searchServices;

        public HomeController(ISearchServices searchServices)
        {
            if (searchServices == null)
                throw new ArgumentNullException("searchServices");

            _searchServices = searchServices;
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            AdSearchResultModel result = _searchServices.SearchAds(new AdSearchResultModel());
            return View(result);
        }

        //
        // GET: /Home/Search
        public ActionResult Search(AdSearchModel model)
        {
            AdSearchResultModel result = _searchServices.SearchAds(model);
            return View("Index", result);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bea.core.Services;
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
            IList<AdSearchResultModel> result = _searchServices.SearchAdsByTitle(null);

            return View(result);
        }

    }
}

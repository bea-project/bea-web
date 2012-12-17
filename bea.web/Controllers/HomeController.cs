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
        public ActionResult Index(String title)
        {
            AdSearchResultModel result = _searchServices.SearchAdsByTitle(title);
            return View(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Bea.Core.Services;
using Bea.Models;
using Bea.Domain.Categories;
using Bea.Domain.Ads;
using Bea.Models.Search;
using System.Threading.Tasks;

namespace Bea.Web.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {
         
        }

        public ActionResult Index()
        {
            return View(new AdSearchModel());
        }
    }
}

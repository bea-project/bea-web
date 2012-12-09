using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bea.Core.Services;

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
        public ActionResult Index()
        {
            var result = _adServices.CountAdsByCities();
            return View(result);
        }

    }
}

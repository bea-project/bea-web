using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Bea.Core.Dal;
using Bea.Domain.Ads;
using Bea.Domain.Search;

namespace Bea.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository _repository;

        public AdminController(IRepository repository)
        {
            _repository = repository;
        }

        //
        // GET: /Admin
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/SearchAdCache
        public ActionResult SearchAdCache()
        {
            var result = _repository.GetAll<SearchAdCache>();
            return View(result);
        }

        //
        // GET: /Admin/BaseAd
        public ActionResult BaseAd()
        {
            var result = _repository.GetAll<BaseAd>();
            return View(result);
        }
    }
}

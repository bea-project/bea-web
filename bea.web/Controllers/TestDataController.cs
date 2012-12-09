using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bea.Web.NhibernateHelper;

namespace Bea.Web.Controllers
{
    public class TestDataController : Controller
    {
        private InMemoryDataInjector _inMemoryDataInjector;

        public TestDataController(InMemoryDataInjector inMemoryDataInjector)
        {
            _inMemoryDataInjector = inMemoryDataInjector;
        }

        //
        // GET: /TestData/

        public ActionResult Index()
        {
            _inMemoryDataInjector.InsertInMemoryData();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}

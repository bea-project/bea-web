using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bea.Core.Services;
using Bea.Domain;

namespace Bea.Web.Controllers
{
    public class AdAPIController : ApiController
    {
        private IAdServices _adServices;

        public AdAPIController(IAdServices adServices)
        {
            if (adServices == null)
                throw new ArgumentNullException("adServices");

            _adServices = adServices;
        }
        public AdAPIController()
        { }


        // GET api/AdAPI
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/AdAPI/5
        public Ad Get(int id)
        {
            Ad ad =  _adServices.GetAdById(id);
            return ad;
        }

        // POST api/AdAPI
        public void Post([FromBody]string value)
        {
        }

        // PUT api/AdAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/AdAPI/5
        public void Delete(int id)
        {
        }
    }
}

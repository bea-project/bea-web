using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bea.Core.Services;
using Bea.Domain.Location;

namespace Bea.Web.Controllers
{
    public class LocationApiController : ApiController
    {
        // GET api/locationapi
        private ILocationServices _locationServices;

        public LocationApiController(ILocationServices adServices)
        {
            if (adServices == null)
                throw new ArgumentNullException("locationServices");

            _locationServices = adServices;
        }
        public LocationApiController()
        { }

        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            List<City> cities = _locationServices.GetCitiesFromProvince(id).ToList();
            response = Request.CreateResponse(HttpStatusCode.OK, cities.Select( x=> new {Value=x.Id,Text=x.Label}));
            return response;
        }

        //public HttpResponseMessage Get(long id)
        //{
        //    HttpResponseMessage response;
        //    response = Request.CreateResponse(HttpStatusCode.OK, id);
        //    return response;
        //}
    }
}

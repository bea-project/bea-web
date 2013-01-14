using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bea.Core.Services;
using Bea.Domain.Location;

namespace Bea.Web.Controllers.API
{
    public class LocationApiController : ApiController
    {
        private ILocationServices _locationServices;

        public LocationApiController(ILocationServices adServices)
        {
            if (adServices == null)
                throw new ArgumentNullException("locationServices");

            _locationServices = adServices;
        }

        // GET /api/locationapi/GetAllProvinces
        public HttpResponseMessage GetAllProvinces()
        {
            HttpResponseMessage response;

            var list = _locationServices.GetAllProvinces().Select(x => new { Id = x.Id, Label = x.Label });
            
            response = Request.CreateResponse(HttpStatusCode.OK, list);

            return response;
        }

        // GET /api/locationapi/GetCitiesFromProvince?provinceId=
        public HttpResponseMessage GetCitiesFromProvince(int provinceId)
        {
            HttpResponseMessage response;

            var cities = _locationServices.GetCitiesFromProvince(provinceId).Select(x => new { Id = x.Id, Label = x.Label });

            response = Request.CreateResponse(HttpStatusCode.OK, cities);

            return response;
        }
    }
}

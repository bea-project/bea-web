using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bea.Core.Dal;
using Bea.Domain.Location;

namespace Bea.Web.Controllers.API
{
    public class LocationController : ApiController
    {
        private readonly IRepository _repository;

        public LocationController(IRepository repository)
        {
            _repository = repository;
        }

        public HttpResponseMessage GetAllProvinces()
        {
            HttpResponseMessage response;

            var list = _repository.GetAll<Province>().Select(x => new { Id = x.Id, Label = x.Label });
            
            response = Request.CreateResponse(HttpStatusCode.OK, list);
        
            return response;
        }
    }
}

using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Bea.Web.Controllers.API
{
    public class AdShareAPIController : ApiController
    {

        public HttpResponseMessage GetFacebookInfo()
        {
            HttpResponseMessage response;
            var client = new FacebookClient();
            dynamic me = client.Get("totten");
            string firstName = me.first_name;
            response = Request.CreateResponse(HttpStatusCode.OK, firstName);
            return response;
        }

    }
}

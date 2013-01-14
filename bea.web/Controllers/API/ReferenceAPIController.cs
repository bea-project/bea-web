using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bea.Core.Services;

namespace Bea.Web.Controllers.API
{
    public class ReferenceAPIController : ApiController
    {
        private IReferenceServices _referenceServices;

        public ReferenceAPIController(IReferenceServices referenceServices)
        {
            if (referenceServices == null)
                throw new ArgumentNullException("referenceServices");

            _referenceServices = referenceServices;
        }


    }
}

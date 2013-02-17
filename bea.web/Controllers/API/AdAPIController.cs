using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bea.Core.Services;
using Bea.Core.Services.Ads;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Models.Details;

namespace Bea.Web.Controllers.API
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

        public HttpResponseMessage Get(long id)
        {
            HttpResponseMessage response;
            Ad ad = _adServices.GetAdById(id);
            if (ad == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else 
            {
                AdDetailsModel adModel = new AdDetailsModel(ad);
                response = Request.CreateResponse(HttpStatusCode.OK, adModel);
            }

            return response;
        }

        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            List<BaseAd> ads = _adServices.GetAllAds().ToList();
            List<AdDetailsModel> adsModel = new List<AdDetailsModel>();
            foreach (Ad ad in ads)
            {
                adsModel.Add(new AdDetailsModel(ad));
            }
            response = Request.CreateResponse(HttpStatusCode.OK, adsModel);
            return response;
        }

        // POST api/AdAPI
        public void Post(AdDetailsModel adModel)
        {

        }

        // PUT api/AdAPI/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/AdAPI/5
        public HttpResponseMessage Delete(long id)
        {
            return (Request.CreateResponse(HttpStatusCode.NotImplemented));
        }
    }
}

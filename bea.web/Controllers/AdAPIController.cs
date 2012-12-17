using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bea.Core.Services;
using Bea.Domain;
using Bea.Models;

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

        public AdDetailsModel Get(long id)
        {
            Ad ad = _adServices.GetAdById(id);
            if (ad == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            AdDetailsModel adModel = new AdDetailsModel(ad);
            return adModel;
        }

        public IEnumerable<AdDetailsModel> Get()
        {
            List<Ad> ads = _adServices.GetAllAds().ToList();
            List<AdDetailsModel> adsModel = new List<AdDetailsModel>();
            foreach (Ad ad in ads)
            {
                adsModel.Add(new AdDetailsModel(ad));
            }
            return adsModel;
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
        public void Delete(long id)
        {
            _adServices.DeleteAdById(id);
        }
    }
}

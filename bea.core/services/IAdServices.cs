using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Location;
using Bea.Models;

namespace Bea.Core.Services
{
    public interface IAdServices
    {
        IDictionary<City, int> CountAdsByCities();
        IList<Ad> GetAllAds();
        Ad GetAdById(long adId);
        void DeleteAdById(long adId);
        void AddAd(Ad ad);

        AdDetailsModel GetAdDetails(long adId);
    }
}

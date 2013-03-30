using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Location;
using Bea.Models.Details;
using Bea.Models;
using Bea.Models.Create;

namespace Bea.Core.Services.Ads
{
    public interface IAdServices
    {
        IDictionary<City, int> CountAdsByCities();
        IList<BaseAd> GetAllAds();
        Ad GetAdById(long adId);
        void AddAd(BaseAd ad);
        IList<BaseAd> GetAdsByEmail(String email);
        BaseAd GetAdFromAdCreateModel(AdvancedAdCreateModel model);
    }
}

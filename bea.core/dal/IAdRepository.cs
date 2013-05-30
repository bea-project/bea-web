using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Location;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Search;
using Bea.Domain.Admin;

namespace Bea.Core.Dal
{
    public interface IAdRepository
    {
        /// <summary>
        /// Counts the number of Ads by city
        /// </summary>
        /// <returns>A dictionary of cities and their ad count</returns>
        IDictionary<City, int> CountAdsByCity();

        /// <summary>
        /// Counts the number of Ads by user
        /// </summary>
        /// <returns>A dictionary of users and their ad count</returns>
        IDictionary<User, int> CountAdsByUser();

        /// <summary>
        /// Get all the Ads
        /// </summary>
        /// <returns>A list of Ads</returns>
        IList<BaseAd> GetAllAds();

        /// <summary>
        /// Get all the Ads for given email
        /// </summary>
        /// <returns>A list of Ads</returns>
        IList<BaseAd> GetAdsByEmail(String email);

        /// <summary>
        /// Get a particular Ad by Id and Type
        /// </summary>
        /// <returns>An Ad</returns>
        T GetAdById<T>(long adId) where T : BaseAd;

        /// <summary>
        /// Get the type of an Ad based on its id
        /// </summary>
        /// <param name="adId">The ad Id</param>
        /// <returns>The type of the Ad</returns>
        AdTypeEnum GetAdType(long adId);

        /// <summary>
        /// Returns whether or not an ad can be deleted
        /// (based on whether it exists and if it's not already been deleted)
        /// </summary>
        /// <param name="adId">The ad id to check</param>
        /// <returns>true or false depending on the possibility</returns>
        Boolean CanDeleteAd(long adId);

        /// <summary>
        /// Returns whether or not an ad can be requested as spam
        /// (based on whether it exists and if it's not already been requested)
        /// </summary>
        /// <param name="adId">The ad id to check</param>
        /// <returns>true or false depending on the possibility</returns>
        SpamAdRequest GetSpamRequestAd(long adId);
    }
}

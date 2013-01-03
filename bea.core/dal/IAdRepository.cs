using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Location;
using Bea.Domain;
using Bea.Domain.Ads;

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
        List<Ad> GetAllAds();

        /// <summary>
        /// Get a particular Ad by Id
        /// </summary>
        /// <returns>An Ad</returns>
        Ad GetAdById(long adId);
        
        /// Searches through all the announces by title, body, province or city
        /// using "like" %searchString%
        /// </summary>
        /// <returns></returns>
        IList<Ad> SearchAds(String[] andSearchStrings, String[] orSearchStrings, int? provinceId, int? cityId);

        /// Deletes an Ad by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteAdById(long adId);

        /// Insert an Ad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void AddAd(Ad ad);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Location;
using Bea.Domain;

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
        /// Searches through all the announces by title
        /// using "like" %searchString%
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        IList<Ad> SearchAdsByTitle(String searchString);
    }
}

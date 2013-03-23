using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;
using Bea.Domain.Search;

namespace Bea.Core.Dal
{
    public interface ISearchRepository
    {
        /// <summary>
        /// Searches through all the announces by title, body, city and category
        /// using "like" %searchString%
        /// </summary>
        /// <param name="andSearchStrings"></param>
        /// <param name="cityId"></param>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        IList<SearchAdCache> SearchAds(string[] andSearchStrings = null, int? cityId = null, int[] categoryIds = null);

        /// <summary>
        /// Searches through the SearchAdCache table inner joining on the <typeparam name="T">Vehicle</typeparam> table
        /// while restraining search results over this table with the following restrictions
        /// </summary>
        /// <param name="andSearchStrings">The search terms</param>
        /// <param name="cityId">The city where to look for</param>
        /// <param name="minKm">The minimum Km</param>
        /// <param name="maxKm">The maximum Km</param>
        /// <param name="minYear">The minimum year</param>
        /// <param name="maxYear">The maximum year</param>
        /// <param name="brandId">The brand</param>
        /// <param name="fueldId">The fuel type</param>
        /// <param name="isAuto">The gear type</param>
        /// <param name="engineSizeMin">The minimum engine size</param>
        /// <param name="engineSizeMax">The maximum engine size</param>
        /// <returns></returns>
        IList<SearchAdCache> SearchVehicleAds<T>(
            string[] andSearchStrings,
            int? cityId,
            int[] categorySelectedId,
            int? minKm, int? maxKm,
            int? minYear, int? maxYear,
            int? brandId,
            int? fueldId,
            Boolean? isAuto,
            int? engineSizeMin, int? engineSizeMax) where T : VehicleAd;

    }
}

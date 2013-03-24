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
        /// Searches through the SearchAdCache table inner joining on the <typeparamref name="T">T</typeparamref> table
        /// adding the parameters passed as an object
        /// </summary>
        /// <typeparam name="T">The Ad type where to search for an ad</typeparam>
        /// <param name="parameters">The list of parameters for the search</param>
        /// <returns>The list of ads of type T as SearchAdCache matching the restrictions</returns>
        IList<SearchAdCache> AdvancedSearchAds<T>(AdSearchParameters parameters) where T : BaseAd;

    }
}

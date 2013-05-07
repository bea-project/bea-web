using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models;
using Bea.Models.Search;

namespace Bea.Core.Services
{
    public interface ISearchServices
    {
        /// <summary>
        /// Searches for ads
        /// Automatically detects whether a category or a group category is selected 
        /// or if it is an advanced search with specific parameters
        /// </summary>
        /// <param name="searchQuery">The AdSearchModel (or AdvancedAdSearchModel) containig search parameters</param>
        /// <returns>The list of matching ads</returns>
        AdSearchResultModel SearchAds(AdSearchModel searchQuery);

        /// <summary>
        /// Searches for ads by first detecting the category and city as URL parts
        /// </summary>
        /// <param name="cityLabel">City name</param>
        /// <param name="categoryLabel">Category name</param>
        /// <returns>The list of matching ads (for a category or group and a city)</returns>
        AdSearchResultModel SearchAdsFromUrl(String cityLabel, String categoryLabel);
        
        /// <summary>
        /// Quick searches through all categories and return the number of results per category
        /// </summary>
        /// <param name="searchQuery">The AdSearchModel containig search parameters</param>
        /// <returns>The count of matchings ads per category group and sub categories</returns>
        AdHomeSearchResultModel QuickSearch(AdSearchModel searchQuery);
    }
}

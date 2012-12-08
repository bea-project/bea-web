using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bea.domain.location;

namespace bea.core.dal
{
    public interface IAdRepository
    {
        /// <summary>
        /// Counts the number of Ads by cities
        /// </summary>
        /// <returns>A dictionary of cities and their ad count</returns>
        IDictionary<City, int> CountAdsByCity();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.location;

namespace Bea.Core.dal
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

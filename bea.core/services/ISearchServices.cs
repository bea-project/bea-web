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
        AdSearchResultModel SearchAds(AdSearchModel searchQuery);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models;

namespace Bea.Core.Services
{
    public interface ISearchServices
    {
        AdSearchResultModel SearchAdsByTitle(String title);
        AdSearchResultModel SearchAds(AdSearchModel searchQuery);
    }
}

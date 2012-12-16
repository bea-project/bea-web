using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models;

namespace bea.core.Services
{
    public interface ISearchServices
    {
        AdSearchResultModel SearchAdsByTitle(String title);
    }
}

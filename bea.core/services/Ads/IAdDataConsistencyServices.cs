using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Core.Services.Ads
{
    public interface IAdDataConsistencyServices
    {
        IDictionary<string, string> GetAdDataConsistencyErrors(BaseAd ad);
    }
}

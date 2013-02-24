using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models.Request;

namespace Bea.Core.Services.Ads
{
    public interface IAdRequestServices
    {
        AdRequestModel RequestAds(AdRequestModel model);
    }
}

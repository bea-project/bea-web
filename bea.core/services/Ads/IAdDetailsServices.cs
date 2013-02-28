using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;
using Bea.Models.Create;
using Bea.Models.Details;

namespace Bea.Core.Services.Ads
{
    public interface IAdDetailsServices
    {
        AdDetailsModel GetAdDetails(long adId);
    }
}

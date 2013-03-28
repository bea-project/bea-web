using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;
using Bea.Models;
using Bea.Models.Create;

namespace Bea.Core.Services.Ads
{
    public interface IAdDataConsistencyServices
    {
        //IDictionary<string, string> GetAdDataConsistencyErrors(BaseAd ad);
        IDictionary<string, string> GetAdDataConsistencyErrors(AdvancedAdCreateModel model);
        Dictionary<string, string> GetDataConsistencyErrors(ContactUserFormModel model);
        Boolean IsEmailValid(String email);
    }
}

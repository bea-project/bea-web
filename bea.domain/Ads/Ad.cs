using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Categories;
using Bea.Domain.Location;

namespace Bea.Domain.Ads
{ 
    public class Ad : BaseAd
    {
        public Ad()
            : base()
        {
            AdType = AdTypeEnum.Ad;
        }
    }
}
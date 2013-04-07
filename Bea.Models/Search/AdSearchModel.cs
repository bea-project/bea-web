using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Models.Search
{
    public class AdSearchModel
    {
        public String SearchString { get; set; }
        public int? CitySelectedId { get; set; }
        public int? CategorySelectedId { get; set; }
        public String CategorySelectedLabel { get; set; }
        public String CategoryImagePath { get; set; }
        
        public AdSearchModel()
        {

        }

        public AdSearchModel(AdSearchModel adSearchModel)
        {
            this.SearchString = adSearchModel.SearchString;
            this.CitySelectedId = adSearchModel.CitySelectedId;
            this.CategorySelectedId = adSearchModel.CategorySelectedId;
            this.CategorySelectedLabel = adSearchModel.CategorySelectedLabel;
            this.CategoryImagePath = adSearchModel.CategoryImagePath;
        }
    }
}

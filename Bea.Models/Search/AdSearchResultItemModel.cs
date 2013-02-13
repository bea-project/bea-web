using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Search;

namespace Bea.Models.Search
{
    public class AdSearchResultItemModel
    {
        public long AdId { get; set; }
        public String Title { get; set; }
        public String Location { get; set; }
        public String Price { get; set; }
        public String Category { get; set; }
        public DateTime CreationDate { get; set; }
        public String MainImageId { get; set; }

        public AdSearchResultItemModel()
        {
        }

        public AdSearchResultItemModel(SearchAdCache ad)
        {
            AdId = ad.AdId;
            Title = ad.Title;
            Location = ad.City.Label;
            Category = ad.Category.Label;
            Price = String.Format("{0} Francs", ad.Price);
            CreationDate = ad.CreationDate;
            MainImageId = ad.AdImageId;
        }
    }
}

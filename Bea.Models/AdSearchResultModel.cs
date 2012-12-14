using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;

namespace Bea.Models
{
    public class AdSearchResultModel
    {
        public String Title { get; set; }
        public String Location { get; set; }
        public String Price { get; set; }
        public DateTime CreationDate { get; set; }

        public AdSearchResultModel()
        {
        }

        public AdSearchResultModel(Ad ad)
        {
            Title = ad.Title;
            Location = ad.City.Label;
            Price = String.Format("{0} Francs", ad.Price);
            CreationDate = ad.CreationDate;
        }
    }
}

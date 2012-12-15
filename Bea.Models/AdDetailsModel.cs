using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;

namespace Bea.Models
{
    public class AdDetailsModel
    {
        public long AdId { get; set; }
        public String Title { get; set; }
        public String Location { get; set; }
        public String Price { get; set; }
        public DateTime CreationDate { get; set; }

        public AdDetailsModel()
        {
        }

        public AdDetailsModel(Ad ad)
        {
            AdId = ad.Id;
            Title = ad.Title;
            Location = ad.City.Label;
            Price = String.Format("{0} Francs", ad.Price);
            CreationDate = ad.CreationDate;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;

namespace Bea.Models
{
    public class AdSearchResultItemModel
    {
        public long AdId { get; set; }
        public String Title { get; set; }
        public String Location { get; set; }
        public String Price { get; set; }
        public DateTime CreationDate { get; set; }
        public String MainImageId { get; set; }

        public AdSearchResultItemModel()
        {
        }

        public AdSearchResultItemModel(Ad ad)
        {
            AdId = ad.Id;
            Title = ad.Title;
            Location = ad.City.Label;
            Price = String.Format("{0} Francs", ad.Price);
            CreationDate = ad.CreationDate;

            AdImage primaryImage = ad.Images.Where(i => i.IsPrimary).SingleOrDefault();
            if (primaryImage != null)
                MainImageId = primaryImage.Id.ToString();
        }
    }
}

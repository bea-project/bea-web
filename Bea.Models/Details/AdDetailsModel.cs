﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Bea.Domain.Ads;

namespace Bea.Models.Details
{
    public class AdDetailsModel
    {
        #region BaseAd properties

        public long AdId { get; set; }
        public String Title { get; set; }
        public String Location { get; set; }
        public String Price { get; set; }
        public String Body { get; set; }
        public String CreationDateString { get; set; }
        public String CategoryGroup { get; set; }
        public String Category { get; set; }

        public IList<String> ImagesIds { get; set; }

        public String UserFirstName { get; set; }
        public String UserPhoneNumber { get; set; }

        public Boolean IsNew { get; set; }

        #endregion

        #region BreadCrumb properties

        public String CategoryGroupUrlPart { get; set; }
        public String CategoryUrlPart { get; set; }

        #endregion

        public AdDetailsModel()
        {
            ImagesIds = new List<String>();
        }

        public AdDetailsModel(BaseAd ad)
            : this()
        {
            AdId = ad.Id;
            Title = ad.Title;
            Location = ad.City.Label;
            Price = String.Format(CultureInfo.GetCultureInfo("fr-FR"), "{0:0,0 Francs}", ad.Price);
            CreationDateString = String.Format(CultureInfo.GetCultureInfo("fr-FR"), "{0:f}", ad.CreationDate);
            Body = ad.Body;

            if (ad.Category != null)
            {
                Category = ad.Category.Label;
                CategoryUrlPart = ad.Category.LabelUrlPart;
                CategoryGroup = ad.Category.ParentCategory.Label;
                CategoryGroupUrlPart = ad.Category.ParentCategory.LabelUrlPart;
            }

            UserFirstName = ad.CreatedBy.Firstname;
            UserPhoneNumber = ad.PhoneNumber;

            ad.Images.ToList().ForEach(i => ImagesIds.Add(i.Id.ToString()));
        }
    }
}

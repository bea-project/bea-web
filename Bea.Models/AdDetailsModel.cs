using System;
using System.Collections.Generic;
using System.Globalization;
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
        public String Body { get; set; }
        public DateTime CreationDate { get; set; }

        public IList<String> ImagesIds { get; set; }

        public String UserFirstName { get; set; }
        public String UserPhoneNumber { get; set; }

        public Boolean IsNew { get; set; }

        public AdDetailsModel()
        {
            ImagesIds = new List<String>();
        }

        public AdDetailsModel(Ad ad)
            : this()
        {
            AdId = ad.Id;
            Title = ad.Title;
            Location = ad.City.Label;
            Price = String.Format(CultureInfo.GetCultureInfo("fr-FR"), "{0:0,0 Francs}", ad.Price);
            Body = ad.Body;
            CreationDate = ad.CreationDate;

            UserFirstName = ad.CreatedBy.Firstname;
            UserPhoneNumber = ad.PhoneNumber;

            ad.Images.ToList().ForEach(i => ImagesIds.Add(i.Id.ToString()));
        }
    }
}

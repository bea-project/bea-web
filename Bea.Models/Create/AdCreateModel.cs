using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bea.Domain.Location;
using Bea.Domain.Ads;
using Bea.Domain;

namespace Bea.Models.Create
{
    public class AdCreateModel
    {
        [DisplayName("Titre de l'annonce:")]
        public String Title { get; set; }

        [DisplayName("Texte de l'annonce:")]
        public String Body { get; set; }

        [DisplayName("Prix:")]
        public Double? Price { get; set; }

        [DisplayName("Votre nom:")]
        public String Name { get; set; }
        
        [DisplayName("Email:")]
        public String Email { get; set; }

        [DisplayName("Téléphone:")]
        public String Telephone { get; set; }
        
        [DisplayName("Province:")]
        public int? SelectedProvinceId { get; set; }

        [DisplayName("Ville:")]
        public int? SelectedCityId { get; set; }

        [DisplayName("Type d'annonce:")]
        public Boolean IsOffer { get; set; }

        [DisplayName("Catégorie:")]
        public int? SelectedCategoryId { get; set; }

        public String ImageIds { get; set; }

        public AdCreateModel()
        {
            IsOffer = true;
        }

        public AdCreateModel(BaseAd ad)
            : this()
        {
            Body = ad.Body;
            IsOffer = ad.IsOffer;
            Price = ad.Price;

            if (ad.Category != null)
                SelectedCategoryId = ad.Category.Id;

            if (ad.City != null)
            {
                SelectedCityId = ad.City.Id;
                SelectedProvinceId = ad.City.Province.Id;
            }

            Telephone = ad.PhoneNumber;
            Title = ad.Title;

            if (ad.CreatedBy != null)
            {
                Email = ad.CreatedBy.Email;
                Name = ad.CreatedBy.Firstname;
            }

            // Create the ImageIds list based on the images of the Ad
            StringBuilder sb = new StringBuilder();
            ad.Images.ToList().ForEach(x => sb.AppendFormat("{0};", x.Id.ToString()));
            ImageIds = sb.ToString();
        }
    }
}

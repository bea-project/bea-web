using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bea.Domain.Location;
//using System.Web.Mvc;

namespace Bea.Models
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
        //[Required(ErrorMessage = "Veuillez saisir un nom.")]
        public String Name { get; set; }
        
        [DisplayName("Email:")]
        //[Required(ErrorMessage = "Veuillez insérer une adresse email.")]
        //[RegularExpression("^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$", ErrorMessage = "Email invalide.")]
        public String Email { get; set; }

        [DisplayName("Telephone:")]
        public String Telephone { get; set; }
        
        [DisplayName("Province:")]
        public int? SelectedProvinceId { get; set; }

        [DisplayName("Ville:")]
        public int? SelectedCityId { get; set; }

        [DisplayName("Type d'annonce:")]
        public Boolean IsOffer { get; set; }

        [DisplayName("Ajouter une photo:")]
        public Boolean PicturePath { get; set; }

        [DisplayName("Catégorie:")]
        public int? SelectedCategoryId { get; set; }
    }
}

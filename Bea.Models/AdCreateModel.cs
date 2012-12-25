using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bea.Domain.Location;
using System.Web.Mvc;

namespace Bea.Models
{
    public class AdCreateModel
    {
        [Required(ErrorMessage = "Veuillez donner un titre a votre annonce.")]
        [DisplayName("Titre de l'annonce:")]
        public String Title { get; set; }

        [Required(ErrorMessage = "Veuillez rediger un texte d'annonce.")]
        [DisplayName("Texte de l'annonce:")]
        public String Body { get; set; }

        [DisplayName("Prix:")]
        public Double? Price { get; set; }

        [DisplayName("Votre nom:")]
        [Required(ErrorMessage = "Veuillez saisir un nom.")]
        public String Name { get; set; }
        
        [DisplayName("Email:")]
        [Required(ErrorMessage = "Veuillez inserer une adresse email.")]
        [RegularExpression("^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$", ErrorMessage = "Email invalide.")]
        public String Email { get; set; }

        [DisplayName("Telephone:")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Telephone invalide.")]
        [Required(ErrorMessage = "Veuillez inserer un numero de telephone.")]
        public String Telephone { get; set; }

        public IEnumerable<SelectListItem> Provinces { get; set; }
        
        [DisplayName("Province:")]
        [Required(ErrorMessage = "Veuillez selectionner une province.")]
        public int SelectedProvinceId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        [DisplayName("Ville:")]
        [Required(ErrorMessage = "Veuillez selectionner une ville.")]
        public int SelectedCityId { get; set; }

        [DisplayName("Type d'annonce:")]
        [Required(ErrorMessage = "Veuillez selectionner un type d'annonce.")]
        public Boolean IsOffer { get; set; }

        [DisplayName("Ajouter une photo:")]
        public Boolean PicturePath { get; set; }
    }
}

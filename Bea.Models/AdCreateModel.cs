using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bea.Domain.Location;

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
        [Range(0, 999999999, ErrorMessage = "Veuillez saisir un prix correct.")]
        public Double Price { get; set; }

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

        [DisplayName("Province:")]
        public List<Province> provinces { get; set; }
    }
}

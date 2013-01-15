using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Mvc;

namespace Bea.Models.Create.Vehicules
{
    public class AdVehiculeCreateModel : AdCreateModel
    {
        [DisplayName("Kilometrage:")]
        public int? Km { get; set; }
        
        [DisplayName("Annee-Modele:")]
        public int? SelectedYearId { get; set; }

       
    }
}

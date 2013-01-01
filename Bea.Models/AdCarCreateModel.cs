using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Bea.Models
{
    public class AdCarCreateModel
    {
        public String Brand { get; set; }

        [DisplayName("Marque:")]
        public int SelectedBrand { get; set; }
        public IEnumerable<SelectListItem> BrandsList { get; set; }

        [DisplayName("Carburant:")]
        public int SelectedFuel { get; set; }
        public IEnumerable<SelectListItem> FuelList { get; set; }

        [DisplayName("Kilometrage:")]
        public int Km { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Mvc;
using Bea.Domain.Ads;

namespace Bea.Models.Create.Vehicules
{
    public class AdVehicleCreateModel : AdCreateModel
    {
        [DisplayName("Kilometrage:")]
        public int? Km { get; set; }
        
        [DisplayName("Annee-Modele:")]
        public int? SelectedYearId { get; set; }

        public int Type { get; set; }

        public AdVehicleCreateModel()
            : base()
        {
            this.Type = (int)AdTypeEnum.VehiculeAd;
        }

        public AdVehicleCreateModel(VehicleAd ad)
            : base(ad)
        {
           this.Km = ad.Kilometers;

            if (ad.Year != 0)
                this.SelectedYearId = ad.Year;

            this.Type = (int)AdTypeEnum.VehiculeAd;
        }
    }
}

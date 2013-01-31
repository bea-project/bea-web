using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Bea.Domain.Ads;

namespace Bea.Models.Create.Vehicules
{
    public class AdCarCreateModel : AdVehicleCreateModel
    {
        [DisplayName("Marque:")]
        public int? SelectedBrandId { get; set; }

        [DisplayName("Carburant:")]
        public int? SelectedFuelId { get; set; }

        [DisplayName("Boîte de vitesse:")]
        public bool IsAutomatic { get; set; }

        public AdCarCreateModel()
            : base()
        {
            this.Type = (int)AdTypeEnum.CarAd;
        }

        public AdCarCreateModel(CarAd ad)
            : base(ad)
        {
            this.Type = (int)AdTypeEnum.CarAd;

            if (ad.Fuel != null)
                this.SelectedFuelId = ad.Fuel.Id;
            if (ad.Brand != null)
                this.SelectedBrandId = ad.Brand.Id;
        }

    }
}

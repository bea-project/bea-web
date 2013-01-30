using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Bea.Domain.Ads;

namespace Bea.Models.Create.Vehicules
{
    public class AdOtherVehicleCreateModel : AdVehicleCreateModel
    {
        [DisplayName("Carburant:")]
        public int? SelectedFuelId { get; set; }

        public AdOtherVehicleCreateModel()
            : base()
        {
            this.Type = (int)AdTypeEnum.OtherVehiculeAd;
        }

        public AdOtherVehicleCreateModel(OtherVehicleAd ad)
            : base(ad)
        {
            if (ad.Fuel != null)
                this.SelectedFuelId = ad.Fuel.Id;
            this.Type = (int)AdTypeEnum.OtherVehiculeAd;
        }

    }
}

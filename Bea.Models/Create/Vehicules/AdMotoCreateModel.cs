using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Bea.Domain.Ads;

namespace Bea.Models.Create.Vehicules
{
    public class AdMotoCreateModel : AdVehicleCreateModel
    {
        [DisplayName("Marque:")]
        public int? SelectedBrandId { get; set; }

        [DisplayName("Cylindrée:")]
        public int EngineSize { get; set; }

        public AdMotoCreateModel()
            : base()
        {
            this.Type = (int)AdTypeEnum.MotoAd;
        }

        public AdMotoCreateModel(MotoAd ad)
            : base(ad)
        {
            if (ad.Brand != null)
                this.SelectedBrandId = ad.Brand.Id;
            this.EngineSize = ad.EngineSize;
            this.Type = (int)AdTypeEnum.MotoAd;
        }

    }
}

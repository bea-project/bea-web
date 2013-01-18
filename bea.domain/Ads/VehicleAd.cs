﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.Ads
{
    public abstract class VehicleAd : BaseAd
    {
        public virtual int Year { get; set; }
        public virtual int Kilometers { get; set; }

        public VehicleAd()
            : base()
        {
        }
    }
}

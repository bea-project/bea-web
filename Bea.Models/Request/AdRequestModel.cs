using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Request
{
    public class AdRequestModel
    {
        public String Email { get; set; }
        public Boolean CanRequestAd { get; set; }
        public String InfoMessage { get; set; }
    }
}

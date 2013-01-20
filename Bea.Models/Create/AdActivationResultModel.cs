using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Create
{
    public class AdActivationResultModel
    {
        public long AdId { get; set; }
        public Boolean IsActivated { get; set; }
        public String InfoMessage { get; set; }
    }
}

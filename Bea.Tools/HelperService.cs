using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Bea.Tools
{
    public class HelperService : IHelperService
    {
        private CultureInfo _culture = CultureInfo.GetCultureInfo("fr-FR");

        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public System.Globalization.CultureInfo GetCulture()
        {
            return _culture;
        }
    }
}

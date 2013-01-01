using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Bea.Core.Services
{
    public interface IHelperService
    {
        DateTime GetCurrentDateTime();
        CultureInfo GetCulture();
    }
}

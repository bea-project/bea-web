using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;

namespace Bea.Services
{
    public class HelperService : IHelperService
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}

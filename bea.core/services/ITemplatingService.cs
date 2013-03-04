using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Core.Services
{
    public interface ITemplatingService
    {
        String GetTemplatedDocument(String templateName, IDictionary<String, String> data, IDictionary<String, object[]> list = null);
    }
}

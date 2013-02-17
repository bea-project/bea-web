using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Core.Services
{
    public interface IEmailServices
    {
        void SendEmail(String subject, String content, String toAddress);
    }
}

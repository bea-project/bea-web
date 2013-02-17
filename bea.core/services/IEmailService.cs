using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Core.Services
{
    public interface IEmailService
    {
        void SendEmail(String subject, String content, String toAddress);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Bea.Core.Services;

namespace Bea.Services
{
    public class EmailServices : IEmailServices
    {
        public void SendEmail(string subject, string content, string toAddress, string replyToAddress)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(toAddress);
            mail.Subject = subject;
            mail.ReplyToList.Add(replyToAddress);
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Body = content;

            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.SendAsync(mail, toAddress);
        }
    }
}

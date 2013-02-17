using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Bea.Core.Services;

namespace Bea.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string subject, string content, string toAddress)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("beaprojectnc@gmail.com");
            mail.To.Add(toAddress);
            mail.Subject = subject;
            mail.ReplyToList.Add("no-reply@bea.nc");
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Body = content;

            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.SendAsync(mail, toAddress);
        }
    }
}

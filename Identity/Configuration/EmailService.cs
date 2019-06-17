using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Identity.Configuration
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var msg = new MailMessage {From = new MailAddress("naoresponda@portal.com.br", "Administrador")};

            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.Body = message.Body;
            msg.IsBodyHtml = true;

            var smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SMTP_Server"], Convert.ToInt32(587));
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTP_Username"],
                ConfigurationManager.AppSettings["SMTP_Password"]);
            smtpClient.Credentials = credentials;
            smtpClient.Send(msg);

            return Task.FromResult(0);
        }
    }
}
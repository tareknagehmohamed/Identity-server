using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace IdentityServerAccountJwt.Server.SendEmail
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailsender = "wmam158@gmail.com";
            var password = "tar01273444382";
            var mailmessage = new MailMessage();
            mailmessage.From=new MailAddress(emailsender);
            mailmessage.To.Add(email);
            mailmessage.Subject = subject;
            mailmessage.Body = $"<html><body>{htmlMessage}</body></html>";
            mailmessage.IsBodyHtml = true;
            using (SmtpClient smtpClient=new SmtpClient()) { 
            
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials=new NetworkCredential(emailsender, password);
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.DeliveryMethod= SmtpDeliveryMethod.Network;
                smtpClient.Send(mailmessage);
            }
        }
    }
}

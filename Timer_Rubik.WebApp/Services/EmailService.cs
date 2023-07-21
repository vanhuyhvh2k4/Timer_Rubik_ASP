using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using Timer_Rubik.WebApp.Interfaces;
using MailKit.Net.Smtp;

namespace Timer_Rubik.WebApp.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string toAddress, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("vanhuyhvh2004@gmail.com"));
            email.To.Add(MailboxAddress.Parse(toAddress));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("vanhuyhvh2004@gmail.com", "vkgsszviihylanom");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

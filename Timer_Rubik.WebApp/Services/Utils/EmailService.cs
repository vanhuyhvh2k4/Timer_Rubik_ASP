using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Timer_Rubik.WebApp.Interfaces.Utils;

namespace Timer_Rubik.WebApp.Services.Utils
{
    public class EmailService : IEmailService
    {
        private readonly string host;
        private readonly int port;
        private readonly string username;
        private readonly string password;

        public EmailService(IConfiguration config)
        {
            host = config.GetSection("Mail_Host").Value!;
            port = int.Parse(config.GetSection("Mail_Port").Value!);
            username = config.GetSection("Mail_Username").Value!;
            password = config.GetSection("Mail_Password").Value!;
        }
        public void SendEmail(string toAddress, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(username));
            email.To.Add(MailboxAddress.Parse(toAddress));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(host, port, SecureSocketOptions.StartTls);
            smtp.Authenticate(username, password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

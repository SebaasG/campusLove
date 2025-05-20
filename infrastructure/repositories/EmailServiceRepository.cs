using System.Threading.Tasks;
using campusLove.domain.ports;
using MimeKit;
using MailKit.Net.Smtp;

namespace campusLove.infrastructure.repositories
{
    public class EmailServiceRepository : IEmailRepository
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "campuslovej1@gmail.com"; 
        private readonly string _smtpPass = "toeh yfbm lpbi zoih";

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("CampusLove 💘", _smtpUser));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body }; // <-- Aquí el cambio clave

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpUser, _smtpPass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}

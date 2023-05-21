using BusinessLogic.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace BusinessLogic.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration configuration;

        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendMailAsync(string subject, string html, string to, string? from = null)
        {
            MailData data = configuration.GetSection(nameof(MailData)).Get<MailData>();

            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? data.Email));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(data.Host, data.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(data.Email, data.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
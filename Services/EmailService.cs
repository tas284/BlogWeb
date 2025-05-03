using BlogWeb.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BlogWeb.Services
{
    public class EmailService
    {
        private readonly SmtpConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            IOptions<SmtpConfiguration> configuration,
            ILogger<EmailService> logger)
        {
            _configuration = configuration.Value;
            _logger = logger;
        }

        public async Task<bool> SendAsync(
            string toName,
            string toEmail,
            string subject,
            string body
            )
        {
            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(_configuration.Host, _configuration.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_configuration.Username, _configuration.Password);

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = body,
                    TextBody = body
                };

                var message = new MimeMessage
                {
                    From = { new MailboxAddress(_configuration.FromName, _configuration.FromEmail) },
                    To = { new MailboxAddress(toName, toEmail) },
                    Subject = subject,
                    Body = bodyBuilder.ToMessageBody()
                };

                await client.SendAsync(message);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {ToEmail}", toEmail);
                return false;
            }
        }
    }
}

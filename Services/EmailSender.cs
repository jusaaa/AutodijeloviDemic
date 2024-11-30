using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AutodijeloviDemic.Services
{
    // Klasa za SMTP podešavanja koja se učitavaju iz konfiguracije
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string FromEmail { get; set; }
        public bool EnableSsl { get; set; }
    }

    // Implementacija servisa za slanje e-mailova
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IOptions<EmailSettings> emailSettings, ILogger<EmailSender> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Validacija ulaznih podataka
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Recipient email cannot be null or empty.", nameof(email));
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Subject cannot be null or empty.", nameof(subject));
            if (string.IsNullOrWhiteSpace(htmlMessage))
                throw new ArgumentException("Message cannot be null or empty.", nameof(htmlMessage));

            // Kreiranje poruke
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail,"AutodijeloviDemic"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            // Kreiranje SMTP klijenta
            using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword),
                EnableSsl = _emailSettings.EnableSsl
            };

            try
            {
                // Slanje poruke
                await client.SendMailAsync(mailMessage);
                _logger.LogInformation($"Email sent successfully to {email} with subject '{subject}'.");
            }
            catch (Exception ex)
            {
                // Logovanje greške
                _logger.LogError(ex, $"Failed to send email to {email} with subject '{subject}'.");
                throw new InvalidOperationException("Error sending email", ex);
            }
        }
    }
}

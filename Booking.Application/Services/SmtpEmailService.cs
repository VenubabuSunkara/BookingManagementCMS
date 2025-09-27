using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace Booking.Application.Services
{
    public class SmtpEmailService(IOptions<EmailSettings> options, ILogger<SmtpEmailService> logger) : ISmtpEmailService
    {
        private readonly EmailSettings _settings = options.Value;
        private readonly ILogger<SmtpEmailService> _logger = logger;

        public async Task SendEmailAsync(ISmtpEmailService.EmailMessage message)
        {
            try
            {
                var msg = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                    Subject = message.Subject,
                    Body = message.HtmlContent,
                    IsBodyHtml = true
                };
                msg.To.Add(message.To);

                using var client = new SmtpClient(_settings.SmtpServer, _settings.Port)
                {
                    Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                    EnableSsl = true
                };

                await client.SendMailAsync(msg);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Send Email error: {ex}");
            }
        }
    }
}

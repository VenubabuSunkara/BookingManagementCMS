using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Booking.Application.Services
{
    public class SendGridEmailService(ISendGridClient client, IOptions<SendGridSettings> options, ILogger<SendGridEmailService> logger) : ISendGridEmailService
    {
        private readonly ISendGridClient _client = client;
        private readonly SendGridSettings _settings = options.Value;
        private readonly ILogger<SendGridEmailService> _logger = logger;
        public async Task SendEmailAsync(ISendGridEmailService.EmailMessage message)
        {
            try
            {
                var From = new EmailAddress(_settings.SenderEmail, _settings.SenderName);
                var To = new EmailAddress(message.To);
                var email = MailHelper.CreateSingleEmail(From, To, message.Subject, message.PlainText, message.HtmlContent);
                await _client.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Send Email error: {ex}");
            }
        }
    }
}

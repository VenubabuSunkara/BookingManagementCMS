using Booking.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class SendGridEmailService(ISendGridClient client, IConfiguration config, ILogger<SendGridEmailService> logger) : IEmailEmailQueueService
    {
        private readonly ISendGridClient _client = client;
        private readonly string _fromEmail = config["SendGrid:FromEmail"]!;
        private readonly string _fromName = config["SendGrid:FromName"]!;
        private readonly ILogger<SendGridEmailService> _logger = logger;

        public async Task SendEmailAsync(IEmailEmailQueueService.EmailMessage msg)
        {
            try
            {
                var from = new EmailAddress(_fromEmail, _fromName);
                var to = new EmailAddress(msg.To);
                var email = MailHelper.CreateSingleEmail(from, to, msg.Subject, msg.PlainText, msg.HtmlContent);
                await _client.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Send Email error: {ex}");
            }
        }
    }
}

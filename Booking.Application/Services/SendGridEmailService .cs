using Booking.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly ISendGridClient _client;
        private readonly string _fromEmail;
        private readonly string _fromName;
        public SendGridEmailService(ISendGridClient client, IConfiguration config)
        {
            _client = client;
            _fromEmail = config["SendGrid:FromEmail"]!;
            _fromName = config["SendGrid:FromName"]!;
        }
        public async Task SendEmailAsync(IEmailService.EmailMessage msg)
        {
            var from = new EmailAddress(_fromEmail, _fromName);
            var to = new EmailAddress(msg.To);
            var email = MailHelper.CreateSingleEmail(from, to, msg.Subject, msg.PlainText, msg.HtmlContent);
            await _client.SendEmailAsync(email);
        }
    }
}

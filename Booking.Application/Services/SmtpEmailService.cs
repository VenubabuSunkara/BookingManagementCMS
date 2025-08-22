using Booking.Application.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Booking.Application.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly FormatOptions _settings;
        public SmtpEmailService(IOptions<FormatOptions> options)
        {
            _settings = options.Value;
        }
        public Task SendEmailAsync(IEmailService.EmailMessage message)
        {
            throw new NotImplementedException();
        }
    }
}

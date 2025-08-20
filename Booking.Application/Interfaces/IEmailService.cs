using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface IEmailService
    {
        public record EmailMessage(string To, string Subject, string PlainText, string? HtmlContent = null);
        Task SendEmailAsync(EmailMessage message);
    }
}

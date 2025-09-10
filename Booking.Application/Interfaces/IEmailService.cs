namespace Booking.Application.Interfaces
{
    public interface IEmailService
    {
        public record EmailMessage(string To, string Subject, string? PlainText = null, string? HtmlContent = null);
        Task SendEmailAsync(EmailMessage message);
    }
}

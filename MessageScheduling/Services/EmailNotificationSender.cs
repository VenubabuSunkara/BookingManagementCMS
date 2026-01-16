using MessageScheduling.Interface;
using MessageScheduling.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace MessageScheduling.Services
{
    public class EmailNotificationSender : INotificationSender
    {
        private readonly ILogger<EmailNotificationSender> _logger;
        private readonly SmtpSettings _smtpSettings;

        public EmailNotificationSender(
            ILogger<EmailNotificationSender> logger,
            IOptions<SmtpSettings> smtpSettings)
        {
            _logger = logger;
            _smtpSettings = smtpSettings.Value;
        }

        public async Task<NotificationResult> SendAsync(INotification notification)
        {
            try
            {
                var emailNotification = (EmailNotification)notification;
                using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
                {
                    EnableSsl = _smtpSettings.EnableSsl,
                    Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password)
                };

                var message = new MailMessage
                {
                    From = new MailAddress(emailNotification.From),
                    Subject = emailNotification.Subject,
                    Body = emailNotification.Body,
                    IsBodyHtml = emailNotification.IsHtml
                };

                emailNotification.To.ForEach(to => message.To.Add(to));
                emailNotification.Cc.ForEach(cc => message.CC.Add(cc));
                emailNotification.Bcc.ForEach(bcc => message.Bcc.Add(bcc));

                foreach (var attachment in emailNotification.Attachments)
                {
                    var attachmentStream = new MemoryStream(attachment.Content);
                    message.Attachments.Add(new Attachment(attachmentStream, attachment.FileName, attachment.ContentType));
                }

                await client.SendMailAsync(message);
                return new NotificationResult(true, notification.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email notification");
                return new NotificationResult(false, notification.Id, ex.Message);
            }
        }
    }
}

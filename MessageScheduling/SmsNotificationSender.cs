using MessageScheduling.Interface;
using MessageScheduling.Interfaces;
using MessageScheduling.Models;
using Microsoft.Extensions.Logging;

namespace MessageScheduling
{
    public class SmsNotificationSender(
        ILogger<SmsNotificationSender> logger,
        ITwilioClient twilioClient) : INotificationSender
    {
        private readonly ILogger<SmsNotificationSender> _logger = logger;
        private readonly ITwilioClient _twilioClient = twilioClient;

        public async Task<NotificationResult> SendAsync(INotification notification)
        {
            try
            {
                var smsNotification = (SmsNotification)notification;
                var result = await _twilioClient.SendSmsAsync(
                   
                    smsNotification.SenderId,
                    smsNotification.PhoneNumber,
                    smsNotification.Body);

                return new NotificationResult(true, result.Sid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send SMS notification");
                return new NotificationResult(false, notification.Id, ex.Message);
            }
        }
    }


}

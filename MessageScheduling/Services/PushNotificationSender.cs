using FirebaseAdmin.Messaging;
using MessageScheduling.Interface;
using MessageScheduling.Models;
using Microsoft.Extensions.Logging;

namespace MessageScheduling.Services
{
    public class PushNotificationSender : INotificationSender
    {
        private readonly ILogger<PushNotificationSender> _logger;
        private readonly FirebaseMessaging _firebaseMessaging;

        public PushNotificationSender(
            ILogger<PushNotificationSender> logger,
            FirebaseMessaging firebaseMessaging)
        {
            _logger = logger;
            _firebaseMessaging = firebaseMessaging;
        }

        public async Task<NotificationResult> SendAsync(INotification notification)
        {
            try
            {
                var pushNotification = (PushNotification)notification;
                var message = new Message
                {
                    Token = pushNotification.DeviceToken,
                    Notification = new Notification
                    {
                        Title = pushNotification.Subject,
                        Body = pushNotification.Body
                    },
                    Data = pushNotification.CustomData
                };

                var result = await _firebaseMessaging.SendAsync(message);
                return new NotificationResult(true, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send push notification");
                return new NotificationResult(false, notification.Id, ex.Message);
            }
        }
    }
}

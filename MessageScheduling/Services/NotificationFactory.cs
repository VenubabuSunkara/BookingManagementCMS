using MessageScheduling.Configurations;
using MessageScheduling.Interface;
using MessageScheduling.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MessageScheduling.Services
{
    public class NotificationFactory : INotificationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public INotificationSender CreateSender(NotificationType type)
        {
            return type switch
            {
                NotificationType.Email => _serviceProvider.GetRequiredService<EmailNotificationSender>(),
                NotificationType.SMS => _serviceProvider.GetRequiredService<SmsNotificationSender>(),
                NotificationType.PushNotification => _serviceProvider.GetRequiredService<PushNotificationSender>(),
                NotificationType.WindowsNotification => _serviceProvider.GetRequiredService<WindowsNotificationSender>(),
                _ => throw new ArgumentException($"Unsupported notification type: {type}")
            };
        }

        public INotification CreateNotification(NotificationType type)
        {
            return type switch
            {
                NotificationType.Email => new EmailNotification
                {
                    From = string.Empty,
                    To = []
                },
                NotificationType.SMS => new SmsNotification
                {
                    PhoneNumber = string.Empty // Set required member
                },
                NotificationType.PushNotification => new PushNotification
                {
                    DeviceToken = string.Empty,
                    Platform = string.Empty
                },
                NotificationType.WindowsNotification => new WindowsNotification
                {
                    ApplicationId = string.Empty,
                    UserId = string.Empty
                },
                _ => throw new ArgumentException($"Unsupported notification type: {type}")
            };
        }
    }
}

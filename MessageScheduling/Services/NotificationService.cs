using MessageScheduling.Configurations;
using MessageScheduling.Interface;
using MessageScheduling.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationFactory _factory;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            INotificationFactory factory,
            ILogger<NotificationService> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public async Task<NotificationResult> SendAsync(INotification notification, NotificationType type)
        {
            try
            {
                var sender = _factory.CreateSender(type);
                return await sender.SendAsync(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send notification");
                return new NotificationResult(false, notification.Id, ex.Message);
            }
        }

        public async Task<IEnumerable<NotificationResult>> SendBulkAsync(
            IEnumerable<INotification> notifications,
            NotificationType type)
        {
            var sender = _factory.CreateSender(type);
            var tasks = notifications.Select(n => sender.SendAsync(n));
            return await Task.WhenAll(tasks);
        }
    }

}

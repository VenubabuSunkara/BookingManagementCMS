using MessageScheduling.Configurations;
using MessageScheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Interface
{
    public interface INotificationService
    {
        Task<NotificationResult> SendAsync(INotification notification, NotificationType type);
        Task<IEnumerable<NotificationResult>> SendBulkAsync(IEnumerable<INotification> notifications, NotificationType type);
    }
}

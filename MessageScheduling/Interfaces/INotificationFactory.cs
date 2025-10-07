using MessageScheduling.Configurations;

namespace MessageScheduling.Interface
{
    public interface INotificationFactory
    {
        INotificationSender CreateSender(NotificationType type);
        INotification CreateNotification(NotificationType type);
    }

}

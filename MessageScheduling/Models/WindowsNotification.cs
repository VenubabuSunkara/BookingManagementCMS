using MessageScheduling.Configurations;

namespace MessageScheduling.Models
{
    public sealed record WindowsNotification : BaseNotification
    {
        public WindowsNotification(string? subject = null, string? body = null)
            : base(subject, body)
        {
        }

        public required string ApplicationId { get; init; }
        public required string UserId { get; init; }
        public NotificationPriority Priority { get; init; } = new();

        public  bool Validate()
        {
            return 
            !string.IsNullOrEmpty(ApplicationId) &&
            !string.IsNullOrEmpty(UserId);
        }
    }
}

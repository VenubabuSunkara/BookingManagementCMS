using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Models
{
    public sealed record PushNotification : BaseNotification
    {
        public PushNotification(string? subject = null, string? body = null)
            : base(subject, body)
        {
        }

        public required string DeviceToken { get; init; }
        public required string Platform { get; init; }
        public Dictionary<string, string> CustomData { get; init; } = new();

        public bool Validate()
        {
            return
            !string.IsNullOrEmpty(DeviceToken) &&
            !string.IsNullOrEmpty(Platform);
        }
    }

}

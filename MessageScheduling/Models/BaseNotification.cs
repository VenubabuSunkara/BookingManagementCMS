using MessageScheduling.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Models
{
    public record class BaseNotification : INotification
    {
        public BaseNotification(string? subject = null, string? body = null)
        {
            Id = Guid.NewGuid().ToString();
            Subject = subject ?? string.Empty;
            Body = body ?? string.Empty;
        }

        public string Id { get; init; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Dictionary<string, object> Metadata { get; } = new();
    }

}

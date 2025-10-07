using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Models
{
    public sealed record SmsNotification : BaseNotification
    {
        public SmsNotification(string? body = null, string phoneNumber = "", string senderId = "")
            : base(body: body)
        {
            PhoneNumber = phoneNumber;
            SenderId = senderId;
        }

        public string PhoneNumber { get; init; }
        public string SenderId { get; init; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(PhoneNumber);
        }
    }
}

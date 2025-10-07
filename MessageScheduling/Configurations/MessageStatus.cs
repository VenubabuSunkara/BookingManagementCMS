using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Configurations
{
    // Enums for status
    public enum MessageStatus
    {
        Accepted,
        Queued,
        Sending,
        Sent,
        Failed,
        Delivered,
        Undelivered
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Configurations
{
    public enum CallStatus
    {
        Queued,
        Ringing,
        InProgress,
        Completed,
        Failed,
        Busy,
        NoAnswer,
        Canceled
    }
}

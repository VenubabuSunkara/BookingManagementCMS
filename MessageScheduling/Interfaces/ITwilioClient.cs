using MessageScheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Interfaces
{
    // Interface
    public interface ITwilioClient
    {
        Task<MessageResource> SendSmsAsync(string to, string from, string message);
        Task<CallResource> MakeCallAsync(string to, string from, string url);
        Task<MessageResource> GetMessageStatusAsync(string messageSid);
        Task<CallResource> GetCallStatusAsync(string callSid);
    }
}

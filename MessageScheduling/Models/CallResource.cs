using MessageScheduling.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Models
{
    // Call model that can be used instead of Twilio.Rest.Api.V2010.CallResource
    public class CallResource
    {
        public string Sid { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public CallStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Price { get; set; }
        public string ErrorMessage { get; set; }
    }
}

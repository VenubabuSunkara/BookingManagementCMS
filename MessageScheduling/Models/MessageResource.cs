using MessageScheduling.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;

namespace MessageScheduling.Models
{
    // Message model that can be used instead of Twilio.Rest.Api.V2010.MessageResource
    public class MessageResource
    {
        public string Sid { get; set; }
        public string Body { get; set; }
        public PhoneNumber From { get; set; }
        public string To { get; set; }
        public MessageStatus Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateSent { get; set; }
        public string Price { get; set; }
        public string ErrorMessage { get; set; }
    }
}

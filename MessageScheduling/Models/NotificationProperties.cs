using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Models
{
    public record NotificationResult(bool IsSuccess, string MessageId, string ErrorMessage = null);
    public record NotificationAttachment(string FileName, byte[] Content, string ContentType);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Models
{
    public sealed record EmailAttachment(string FileName, byte[] Content, string ContentType)
    {
        public static EmailAttachment Create(string fileName, byte[] content, string contentType)
        {
            ArgumentException.ThrowIfNullOrEmpty(fileName);
            ArgumentNullException.ThrowIfNull(content);
            ArgumentException.ThrowIfNullOrEmpty(contentType);
            return new(fileName, content, contentType);
        }
    }
}

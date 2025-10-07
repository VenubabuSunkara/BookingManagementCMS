using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Models
{
    // Derived notification types
    public sealed record EmailNotification : BaseNotification
    {
        public EmailNotification(string? subject = null, string? body = null)
            : base(subject, body)
        {
            Attachments = [];
        }

        public required string From { get; init; }
        public required List<string> To { get; init; } = [];
        public List<string> Cc { get; init; } = [];
        public List<string> Bcc { get; init; } = [];
        public bool IsHtml { get; init; }
        public ImmutableList<EmailAttachment> Attachments { get; private set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(From) && To.Count != 0;
        }
        public EmailNotification WithAttachment(EmailAttachment attachment)
        {
            Attachments = Attachments.Add(attachment);
            return this;
        }

        public EmailNotification WithAttachments(IEnumerable<EmailAttachment> attachments)
        {
            Attachments = Attachments.AddRange(attachments);
            return this;
        }

        public EmailNotification WithoutAttachment(string fileName)
        {
            Attachments = Attachments.RemoveAll(a => a.FileName == fileName);
            return this;
        }

        public EmailNotification ClearAttachments()
        {
            Attachments = ImmutableList<EmailAttachment>.Empty;
            return this;
        }
    }
}

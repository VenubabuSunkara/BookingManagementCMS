using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class EmailTemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailBody { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdatedOn { get; set; }
        public bool IsEnabled { get; set; }
        public string SenderEmail { get; set; } = string.Empty;
    }
}

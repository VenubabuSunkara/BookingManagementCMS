using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class EmailTemplateTableDto
    {
        public int TotalRecords { get; set; }
        public int FilterRecords { get; set; }
        public IEnumerable<EmailTemplateDto> EmailTemplatesDto { get; set; } = [];
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class SettingsDto
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set; }
        public int Id { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

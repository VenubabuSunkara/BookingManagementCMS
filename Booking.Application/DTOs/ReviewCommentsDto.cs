using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class ReviewCommentsDto
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public decimal? Rating { get; set; }

        public int? DriverId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
    }
}

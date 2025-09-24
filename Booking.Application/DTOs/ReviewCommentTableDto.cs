using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class ReviewCommentTableDto
    {
        public int Total { get; set; }
        public int Filtered { get; set; }
        public IEnumerable<ReviewCommentsDto> ReviewComments { get; set; } = [];
    }
}

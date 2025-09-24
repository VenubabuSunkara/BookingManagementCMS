using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class ReviewCommentTableEntity
    {
        public int Total { get; set; }
        public int Filtered { get; set; }
        public IEnumerable<ReviewCommentEntity> ReviewComments { get; set; } = [];
    }
}

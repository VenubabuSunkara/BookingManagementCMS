using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class DriverRatingEntity
    {
        public int RatingId { get; set; }
        public int? DriverId { get; set; }
        public int? PassengerId { get; set; }
        public int? Rating { get; set; }
        public string? Comments { get; set; }
    }
}

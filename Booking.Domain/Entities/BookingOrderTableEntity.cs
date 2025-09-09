using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class BookingOrderTableEntity
    {
        public int FilterRecords { get; set; } = 0;
        public int TotalRecords { get; set; } = 0;
        public IEnumerable<BookingOrderEntity> BookingOrderEntities { get; set; } = [];

    }
}

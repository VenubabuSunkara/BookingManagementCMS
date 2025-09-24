using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class PaymentEntity
    {
        public int PaymentId { get; set; }
        public int BookingOrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; } = null!;
        public DateTime? PaymentDate { get; set; }
        public string Status { get; set; } = null!;
    }
}

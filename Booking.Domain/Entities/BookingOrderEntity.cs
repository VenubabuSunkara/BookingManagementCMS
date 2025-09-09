using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class BookingOrderEntity
    {
        public int BookingOrderId { get; set; }

        public string BookingNumber { get; set; } = null!;

        public int CustomerId { get; set; }

        public int? DriverId { get; set; }

        public int? VehicleId { get; set; }

        public DateTime BookingDate { get; set; }

        public string PickupLocation { get; set; } = null!;

        public string DropLocation { get; set; } = null!;

        public string TripType { get; set; } = null!;

        public string Status { get; set; } = null!;

        public decimal? EstimatedFare { get; set; }

        public decimal? ActualFare { get; set; }

        public string PaymentStatus { get; set; } = null!;

        public DateTime? ScheduledPickupTime { get; set; }

        public DateTime? ScheduledDropTime { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}

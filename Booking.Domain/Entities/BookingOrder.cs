using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class BookingOrder
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int PackageId { get; set; }

        public int? CouponCodeId { get; set; }

        public int? VehicleId { get; set; }

        public DateTime? BookingDate { get; set; }

        public DateTime? TravelDate { get; set; }

        public string? Status { get; set; }

        public decimal? TotalAmount { get; set; }

        public ICollection<BookingDetailsEntity> BookingDetails { get; set; } = new List<BookingDetailsEntity>();
        public Vehicle Vehicle { get; set; } = new Vehicle();
        public Driver Driver { get; set; } = new Driver();
    }
    public class BookingsDTable
    {
        public int Total { get; set; }
        public int Filtered { get; set; }
        public Vehicle Vehicle { get; set; } = new Vehicle();
        public Driver Driver { get; set; } = new Driver();
        public IEnumerable<BookingOrder> BookingOrders { get; set; } = [];

    }
}

using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class BookingOrderDto
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

        public ICollection<BookingDetailsDto> BookingDetails { get; set; } = new List<BookingDetailsDto>();
    }
}

using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class BookingsDataTableDto
    {
        public int TotalRecords { get; set; }
        public int FilterRecords { get; set; }
        public IEnumerable<BookingOrderDto> BookingsInfo { get; set; } = [];
        public string NextLink { get; set; } = string.Empty;
        public string PrevLink { get; set; } = string.Empty;
    }
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
        public DriverDto Driver { get; set; } = new DriverDto();
        public VehicleDto Vehicle { get; set; } = new VehicleDto();
        public ICollection<BookingDetailsDto> BookingDetails { get; set; } = [];
    }
}

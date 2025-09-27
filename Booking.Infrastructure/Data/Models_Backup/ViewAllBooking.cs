using System;
using System.Collections.Generic;

namespace Booking.Infrastructure.Data.Models;

public partial class ViewAllBooking
{
    public int BookingOrderId { get; set; }

    public string BookingNumber { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string? DriverName { get; set; }

    public string? VehicleNumber { get; set; }

    public string? ModelName { get; set; }

    public string PickupLocation { get; set; } = null!;

    public string DropLocation { get; set; } = null!;

    public string Status { get; set; } = null!;

    public decimal? EstimatedFare { get; set; }

    public decimal? ActualFare { get; set; }
}

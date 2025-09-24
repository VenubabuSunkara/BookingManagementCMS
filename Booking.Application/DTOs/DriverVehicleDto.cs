using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class DriverVehicleDto
    {
        public DriverDto Driver { get; set; } = new DriverDto();
        public VehicleDto Vehicle { get; set; } = new VehicleDto();
    }
}

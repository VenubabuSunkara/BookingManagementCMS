using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class NewDriverVehicleDto
    {

        public DriverDto Driver { get; set; }
        public VehicleDto Vehicle { get; set; }
        public VehicleMediaDto VehicleMedia { get; set; }
        public NewDriverVehicleDto()
        {
            Driver = new DriverDto();
            Vehicle = new VehicleDto();
            VehicleMedia = new VehicleMediaDto();
        }
    }
}

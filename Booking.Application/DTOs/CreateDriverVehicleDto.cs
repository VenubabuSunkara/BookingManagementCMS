using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class CreateDriverVehicleDto
    {
        #region Driver
        public int DriverId { get; set; }
        public string DriverFirstName { get; set; } = string.Empty;
        public string DriverLastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string AboutOnDriver { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string DriverPhotoUrl { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        #endregion
        #region Vehicle
        public int VehicleId { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string VehicleNumber { get; set; } = string.Empty;
        public string AboutOnVehicle { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string FualType { get; set; } = string.Empty;
        public decimal BasePrice { get; set; } = 0.0M;
        public decimal TaxRate { get; set; } = 0.0M;
        public string OtherInformation { get; set; } = string.Empty;
        public string DefaultImage { get; set; } = string.Empty;
        #endregion

    }
}

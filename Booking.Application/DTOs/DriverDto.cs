using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Address { get; set; }
        public bool? AvailabilityStatus { get; set; }
        public Guid? TenantId { get; set; }
        public string? AboutOn { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public string? Photo { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public bool IsDriverAvailable => AvailabilityStatus == true;
        public bool? IsApproved { get; set; }
        public bool? IsVehicleAssigned { get; set; }
        public int NoTripsDone { get; set; }
        public int AvgRating { get; set; }
        public VehicleDto? Vehicle { get; set; }
        public List<VehicleMediaDto> VehicleMedias { get; set; } = [];
        public List<FeatureDto> VehicleFeatures { get; set; } = [];
    }
}

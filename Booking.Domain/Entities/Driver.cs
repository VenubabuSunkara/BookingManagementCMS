using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class Driver
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? LicenseNumber { get; set; }

        public string? Address { get; set; }

        public string? VehicleType { get; set; }

        public bool? AvailabilityStatus { get; set; }

        public string? UserName { get; set; }

        public byte[]? PasswordHash { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? TenantId { get; set; }

        public string? AboutOn { get; set; }

        public string? Photo { get; set; }
        public string GetFullName() => $"{FirstName} {LastName}";
        public bool IsDriverAvailable() => AvailabilityStatus == true;

    }
}
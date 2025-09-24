using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class DriverExportEntity
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
        public bool? IsApproved { get; set; }
        public float Rating { get; set; }
        public float NoTripsDone { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class CreateDriverEntity
    {
        public int DriverId { get; set; }

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Email { get; set; }

        public string LicenseNumber { get; set; } = null!;

        public string? Address { get; set; }

        public bool? AvailabilityStatus { get; set; }

        public string? AboutOn { get; set; }

        public string? Photo { get; set; }

        public bool? ApproveDriver { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedOn { get; set; }

        public Guid TenantId { get; set; }

        public Guid ItemGuid { get; set; }

        public bool? IsActive { get; set; }

        public string? Gender { get; set; }

        public string? DateOfBirth { get; set; }
    }
    public class CreateVehicleEntity
    {
        public int VehicleId { get; set; }

        public string VehicleNumber { get; set; } = null!;

        public string? AboutOnVehicle { get; set; }

        public string Color { get; set; } = null!;

        public string Model { get; set; } = null!;

        public decimal Fare { get; set; }

        public string CreatedBy { get; set; } = null!;

        public string UpdatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string DefaultImage { get; set; } = null!;

        public Guid ItemGuid { get; set; }

        public string? CarName { get; set; }

        public int? AverageMileage { get; set; }

        public string? InsurnceNumber { get; set; }

        public string? InsurenceValidUntil { get; set; }

        public string? PollucationCertificationNumber { get; set; }

        public string? Fecility { get; set; }

        public int? VehicleTypeId { get; set; }
    }
    public class CreateDriverVehicleEntity
    {
        public string DriverPhotojson { get; set; } = string.Empty;
        public string VehicleDefaultImagejson { get; set; } = string.Empty;
        public CreateDriverEntity DriverEntity { get; set; } = new CreateDriverEntity();
        public CreateVehicleEntity VehicleEntity { get; set; } = new CreateVehicleEntity();
    }

}

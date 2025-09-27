using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class UserEntity
    {
        public Guid TenantId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; } = string.Empty;

        public string RoleId { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = [];
        public string ProfilePhoto { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
        public string RegistrationToken { get; set; } = string.Empty;
    }
}

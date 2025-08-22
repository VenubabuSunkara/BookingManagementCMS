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
        public int Id { get; set; }
        public string Contact { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}

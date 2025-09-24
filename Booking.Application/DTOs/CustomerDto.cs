using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
    public class CustomerDTableDto
    {
        public int Total { get; set; }
        public int Filtered { get; set; }
        public IEnumerable<CustomerDto> CustomerDto { get; set; } = [];

    }
    public class CustomerPassordDto
    {
        public int CustomerId { get; set; }
        public string NewPassword { get; set; } = null!;

    }
}

using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class UserTableDto
    {
        public int FilterRecords { get; set; } = 0;
        public int TotalRecords { get; set; } = 0;
        public IEnumerable<UserDto> UsersDto { get; set; } = [];

    }
}

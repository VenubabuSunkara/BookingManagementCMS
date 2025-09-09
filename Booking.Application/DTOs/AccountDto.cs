using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class AccountDto
    {
        public IEnumerable<SelectListItem> Roles { get; set; } = [];
        public string RoleId { get; set; } = "";
    }
}

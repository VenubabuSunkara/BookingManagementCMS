using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class RoleDto
    {
        public string Name { get; set; } = null!;
        public string? Id { get; set; } = string.Empty;
        public bool? isEdit { get; set; } = false;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs.Tour
{
    public class TourPackageCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NoOfPackages { get; set; } = 0;
    }
}

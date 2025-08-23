using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs.Tour
{
    public class PackageDataTableDto
    {
        public int TotalRecords { get; set; }
        public int FilterRecords { get; set; }
        public IEnumerable<TourPackageDto> PackagesData { get; set; } = [];
        public string NextLink { get; set; } = string.Empty;
        public string PrevLink { get; set; } = string.Empty;
    }
}

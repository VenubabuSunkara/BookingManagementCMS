
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string? OtherInfromation { get; set; }
        public string? VehicleNumber { get; set; }
        public string? AboutOnVehicle { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? Color { get; set; }
        public string? Make { get; set; }
        public string? ModelName { get; set; }
        public string? DefaultImage { get; set; }
        public string FuelType { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public decimal TaxRate { get; set; }
        public bool? IsActive { get; set; }
    }
}

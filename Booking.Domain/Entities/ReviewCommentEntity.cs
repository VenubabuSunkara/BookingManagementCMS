using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class ReviewCommentEntity
    {
        public int Id { get; set; }
        public string? VehicleComment { get; set; }
        public string? DriverComment { get; set; }
        public string? Suggestions { get; set; }
        public decimal? Rating { get; set; }
        public string? DriverLicense { get; set; }
        public string? VehicleNo { get; set; }
        public int? DriverId { get; set; }
        public int? VehicleId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}

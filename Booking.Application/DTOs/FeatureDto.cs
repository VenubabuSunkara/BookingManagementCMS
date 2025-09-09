using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class FeatureDto
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; } = null!;
        public string FeatureType { get; set; } = null!;
        public string FeatureValue { get; set; } = null!;
        public int VehicleId { get; set; }
    }
}

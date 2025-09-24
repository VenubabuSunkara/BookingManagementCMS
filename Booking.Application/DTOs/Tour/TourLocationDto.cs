using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs.Tour
{
    public class TourLocationDto
    {
        public int? LocationId { get; set; }
        public string? ViaLocations { get; set; }
        public string? LocationHeadLine { get; set; }
        public string? LocationName { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Description { get; set; }
        public string? PointImage { get; set; }
        public int? RouteDistance { get; set; }
        public int? RouteDuration { get; set; }
    }
}

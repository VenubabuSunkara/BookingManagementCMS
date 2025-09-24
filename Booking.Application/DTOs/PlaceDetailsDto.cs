using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class PlaceDetailsDto
    {
        public string PlaceId { get; set; }
        public string Name { get; set; }
        public string FormattedAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Rating { get; set; }
    }
    public class GooglePlaceDetailsResponse
    {
        public GooglePlaceDetailsResult Result { get; set; }
    }

    public class GooglePlaceDetailsResult
    {
        public string Place_Id { get; set; }
        public string Name { get; set; }
        public string Formatted_Address { get; set; }
        public string Formatted_Phone_Number { get; set; }
        public string Website { get; set; }
        public Geometry Geometry { get; set; }
        public double? Rating { get; set; }
    }

    public class Geometry
    {
        public Location Location { get; set; }
    }

    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}

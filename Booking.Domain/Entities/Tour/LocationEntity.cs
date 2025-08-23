using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities.Tour
{
    public class LocationEntity
    {
        public int LocationId { get; set; }

        public string Name { get; set; } = null!;

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }
        // Computed property combining all parts
        public string Destination
        {
            get
            {
                // Gather components, skip null or empty values
                var parts = new[] { Name, City, State, Country }
                    .Where(part => !string.IsNullOrWhiteSpace(part));

                // Join with comma and space
                return string.Join(", ", parts);
            }
        }
    }
}

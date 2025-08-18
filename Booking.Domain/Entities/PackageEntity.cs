using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class PackageEntity
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? ShortDescription { get; set; }

        public string? FullDescription { get; set; }

        public string? Source { get; set; }

        public string? Destination { get; set; }

        public int? DurationDays { get; set; }

        public decimal? Price { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? TurmsandConditions { get; set; }

        public bool? IsActive { get; set; }

        public virtual PackageMediaEntity PackageMedia { get; set; } = new PackageMediaEntity();

    }

    public class PackageDTable
    {
        public int Total { get; set; }
        public int Filtered { get; set; }
        public IEnumerable<PackageEntity> PackageEntities { get; set; } = [];
    }
}

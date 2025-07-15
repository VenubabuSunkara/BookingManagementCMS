using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class VehicleType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        public bool? IsEnabled { get; set; }

        [MaxLength(4000)]
        public string? Description { get; set; }

        public ICollection<Driver>? Drivers { get; set; } // Navigation for reverse lookup

        public int? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public record BaseEntityDto(int Id, DateTime CreatedOn, DateTime UpdateOn, string CreatedBy, string UpdatedBy, Guid ItemGuid);
}

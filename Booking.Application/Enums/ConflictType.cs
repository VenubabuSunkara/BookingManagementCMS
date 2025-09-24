using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Enums
{
    public enum ConflictType
    {
        DriverUnavailable,
        VehicleUnavailable,
        OverlappingSchedule,
        MaintenanceConflict,
        Other
    }
}

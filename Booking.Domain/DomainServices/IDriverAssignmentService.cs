using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.DomainServices
{
    public interface IDriverAssignmentService
    {
        bool CanAssignVehicleToDriver(Driver driver, Driver vehicle);
    }
}

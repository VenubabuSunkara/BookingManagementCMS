using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IDriverVehicleRepository
    {
        Task<DriverVehicleTableEntity> DriverVehicleList(string SearchValue, int Take, int Skip, CancellationToken token);
        Task<int> RejectDriverVehicleAsync(int DriverId, int VehicleId, CancellationToken token);
    }
}

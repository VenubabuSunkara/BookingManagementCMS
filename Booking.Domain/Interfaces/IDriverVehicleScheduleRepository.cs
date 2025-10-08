using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IDriverVehicleScheduleRepository
    {
        Task<DriverVehicleSchedulerTableEntity> DriverVehicleSchedulesList(string ScheduleSearch, int Take, int Skip, CancellationToken token);
        Task<int> RejectDriverVehicleScheduleAsync(int DriverId, int VehicleId, CancellationToken token);
        Task<int> CreateDriverVehicleSchedleAsync(DriverVehicleAvailabilityEntity entity, CancellationToken token);
        Task<int> UpdateDriverVehicleScheduleAsync(DriverVehicleAvailabilityEntity entity, CancellationToken token);
        Task<List<DriverVehicleAvailabilityEntity>> GetDriverVehicleScheduleById(int DriverId, int VehicleId, CancellationToken token);
    }
}

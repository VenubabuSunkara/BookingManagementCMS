using Booking.Application.DTOs;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface IDriverVehicleAvailabilityService
    {
        Task<DriverVehicleSchedulerTableDto> DriverVehicleSchedulesList(string ScheduleSearch, int Take, int Skip, CancellationToken token);
        Task<int> RejectDriverVehicleScheduleAsync(int DriverId, int VehicleId, CancellationToken token);
        Task<int> CreateDriverVehicleSchedleAsync(DriverVehicleScheduleDto entity, CancellationToken token);
        Task<int> UpdateDriverVehicleScheduleAsync(DriverVehicleScheduleDto entity, CancellationToken token);
        Task<List<DriverVehicleScheduleDto>> GetDriverVehicleScheduleById(int DriverId, int VehicleId, CancellationToken token);
    }
}

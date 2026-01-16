using Booking.Application.DTOs;
using Booking.Domain.Entities;

namespace Booking.Application.Interfaces
{
    public interface IDriverVehicleService
    {
        Task<DriverVehicleTableDto> DriverVehicleList(string SearchValue, int Take, int Skip, CancellationToken token);
        Task<int> RejectDriverVehicleAsync(int DriverId, int VehicleId, CancellationToken token);
        Task<int> CreateDriverVehicle(CreateDriverVehicleDto entity, CancellationToken token);
        Task<int> UpdateDriverVehicle(CreateDriverVehicleDto entity, CancellationToken token);
        Task<CreateDriverVehicleDto?> GetDriverVehicleById(int DriverId, int VehicleId, CancellationToken token);
    }
}

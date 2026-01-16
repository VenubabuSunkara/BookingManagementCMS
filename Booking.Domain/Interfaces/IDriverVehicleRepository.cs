using Booking.Domain.Entities;

namespace Booking.Domain.Interfaces
{
    public interface IDriverVehicleRepository
    {
        Task<DriverVehicleTableEntity> DriverVehicleList(string SearchValue, int Take, int Skip, CancellationToken token);
        Task<int> RejectDriverVehicleAsync(int DriverId, int VehicleId, CancellationToken token);
        Task<int> CreateDriverVehicle(CreateDriverVehicleEntity entity, CancellationToken token);
        Task<int> UpdateDriverVehicle(CreateDriverVehicleEntity entity, CancellationToken token);
        Task<CreateDriverVehicleEntity?> GetDriverVehicleById(int DriverId, int VehicleId, CancellationToken token);

    }
}

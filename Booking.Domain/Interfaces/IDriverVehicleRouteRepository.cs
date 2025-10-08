using Booking.Domain.Entities;

namespace Booking.Domain.Interfaces
{
    public interface IDriverVehicleRouteRepository
    {
        Task<DriverVehicleTableEntity> DriverVehicleRouteList(string SearchValue, int Take, int Skip, CancellationToken token);
        Task<int> RejectDriverVehicleRouteAsync(int DriverId, int VehicleId, CancellationToken token);
        Task<int> CreateDriverVehicleRouteAsync(CreateDriverVehicleEntity entity, CancellationToken token);
        Task<int> UpdateDriverVehicleRouteAsync(CreateDriverVehicleEntity entity, CancellationToken token);
        Task<CreateDriverVehicleEntity?> GetDriverVehicleRouteById(int DriverId, int VehicleId, CancellationToken token);

    }
}

using Booking.Application.DTOs;

namespace Booking.Application.Interfaces
{
    public interface IDriverVehicleService
    {
        Task<DriverVehicleTableDto> DriverVehicleList(string SearchValue, int Take, int Skip, CancellationToken token);
        Task<int> RejectDriverVehicleAsync(int DriverId, int VehicleId, CancellationToken token);
    }
}

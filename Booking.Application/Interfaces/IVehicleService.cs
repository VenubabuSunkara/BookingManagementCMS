using Booking.Application.DTOs;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleTableDto> GetVehicleListAsync(string SearchValue, int Take, int Skip, CancellationToken token);
        Task<int> ApproveVehicleAsync(int VehicleId, CancellationToken token);
        Task<int> RejectVehicleAsync(int VehicleId, CancellationToken token);
        Task<int> ApproveVehiclesAsync(List<int> VehicleIds, CancellationToken token);
        Task<int> RejectVehiclesAsync(List<int> VehicleIds, CancellationToken token);
        Task<int> AssignDriverAsync(int DriverId, int VehicleId, CancellationToken token);
        Task<VehicleDto?> GetVehicleAsync(int VehicleId, CancellationToken token);
    }
}

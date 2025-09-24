using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IVehicleRepository
    {
        Task<VehicleTableEntity> GetVehicleListAsync(string SearchValue, int Take, int Skip, CancellationToken token);
        Task<int> ApproveVehicleAsync(int VehicleId, CancellationToken token);
        Task<int> RejectVehicleAsync(int VehicleId, CancellationToken token);
        Task<int> ApproveVehiclesAsync(List<int> VehicleIds, CancellationToken token);
        Task<int> RejectVehiclesAsync(List<int> VehicleIds, CancellationToken token);
        Task<int> AssignDriverAsync(int DriverId, int VehicleId, CancellationToken token);
        Task<VehicleEntity?> GetVehicleAsync(int VehicleId, CancellationToken token);
        Task<IEnumerable<UnAssignedVehiclesEntity>> GetUnAssignedVehiclesList(CancellationToken token);
        Task<bool> AssignDriver(AssignVehicleDriverEntity model, CancellationToken token);
        Task<IEnumerable<VehicleDropdownEntity>> GetVehicleDropdownList(CancellationToken token);
    }
}

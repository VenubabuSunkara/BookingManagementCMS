using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IDriverRepository
    {
        Task<DriverTableEntity> GetDriverListAsync(string SearchValue, int Take, int Skip, CancellationToken token);
        Task<int> ApproveDriverAsync(int DriverId, CancellationToken token);
        Task<int> RejectDriverAsync(int DriverId, CancellationToken token);
        Task<int> ApproveDriversAsync(List<int> DriverIds, CancellationToken token);
        Task<int> RejectDriversAsync(List<int> DriverIds, CancellationToken token);
        Task<int> AssignVehicleAsync(int DriverId, int VehicleId, CancellationToken token);
        Task<DriverEntity?> GetDriverAsync(int DriverId, CancellationToken token);
        Task<IEnumerable<DriverExportEntity>> ExportAllAsync(CancellationToken token);
        Task<IEnumerable<UnAssignedDriversEntity>> GetUnAssignedDriversList(CancellationToken token);
        Task<IEnumerable<DriversDropdownEntity>> GetDriversDropdownList(CancellationToken token);
    }
}

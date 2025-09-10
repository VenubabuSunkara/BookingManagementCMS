using Booking.Application.DTOs;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface IDriverService
    {
        Task<DriverTableDto> GetDriverListAsync(string SearchValue, int Take, int Skip, CancellationToken token);
        Task<int> ApproveDriverAsync(int DriverId, CancellationToken token);
        Task<int> RejectDriverAsync(int DriverId, CancellationToken token);
        Task<int> ApproveDriversAsync(List<int> DriverIds, CancellationToken token);
        Task<int> RejectDriversAsync(List<int> DriverIds, CancellationToken token);
        Task<int> AssignVehicleAsync(int DriverId, int VehicleId, CancellationToken token);
        Task<DriverDto?> GetDriverAsync(int DriverId, CancellationToken token);
        Task<IEnumerable<DriverExportDto>> ExportAllAsync(CancellationToken token);
        Task<IEnumerable<UnAssignedDriversDto>> GetUnAssignedDriversList(CancellationToken token);
    }
}

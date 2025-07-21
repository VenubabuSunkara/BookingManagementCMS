using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IDriverAndVehicleService
    {
        Task<IEnumerable<DriverAndVehicle>> GetAllAsync();
        Task<DriverAndVehicle?> GetByIdAsync(int id);
        Task<IEnumerable<DriverAndVehicle>> GetPagingAsync(int pageNumber, int pageSize);
        Task<IEnumerable<DriverAndVehicle>> SearchAsync(string keyword);
        Task CreateAsync(DriverAndVehicle req);
        Task UpdateAsync(int id, DriverAndVehicle req);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<DriverAndVehicle>> GetAvailableDriversAsync();
        Task ToggleAvailabilityAsync(int id, bool status);
        Task<IEnumerable<DriverAndVehicle>> FilterAsync(string vehicleType, bool? isAvailable);
        Task<int> CountAsync();
    }
}

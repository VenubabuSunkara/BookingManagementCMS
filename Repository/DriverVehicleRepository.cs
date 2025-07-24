using Data;
using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DriverVehicleRepository : IDriverVehicleRepository
    {
        private readonly DriverAndVehicleService _service;

        public DriverVehicleRepository(DriverAndVehicleService service)
        {
            _service = service;
        }

        public async Task<bool> ApproveAsync(int id, bool isApproved)
        {
           return await _service.ApproveAsync(id, isApproved);
        }

        public async Task<int> CountAsync()
        {
            return await _service.CountAsync();
        }

        public async Task CreateAsync(DriverAndVehicle req)
        {
            await _service.CreateAsync(req);
        }

        public async Task DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _service.ExistsAsync(id);
        }

        public async Task<IEnumerable<DriverAndVehicle>> FilterAsync(string vehicleType, bool? isAvailable)
        {
            return await _service.FilterAsync(vehicleType, isAvailable);
        }

        public async Task<IEnumerable<DriverAndVehicle>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

        public async Task<IEnumerable<DriverAndVehicle>> GetAvailableDriversAsync()
        {
            return await _service.GetAvailableDriversAsync();
        }

        public async Task<DriverAndVehicle?> GetByIdAsync(int id)
        {
            return await (_service.GetByIdAsync(id));
        }

        public async Task<IEnumerable<DriverAndVehicle>> GetPagingAsync(int pageNumber, int pageSize)
        {
            return await _service.GetPagingAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<DriverAndVehicle>> SearchAsync(string keyword)
        {
            return await _service.SearchAsync(keyword);
        }

        public async Task ToggleAvailabilityAsync(int id, bool status)
        {
            await _service.ToggleAvailabilityAsync(id, status);
        }

        public async Task UpdateAsync(int id, DriverAndVehicle req)
        {
            await _service.UpdateAsync(id, req);
        }
    }
}

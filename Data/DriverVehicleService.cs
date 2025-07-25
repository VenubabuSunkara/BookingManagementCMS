using Entities;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DriverAndVehicleService : IDriverAndVehicleService
    {
        private readonly BookingManagementCmsContext _context;

        public DriverAndVehicleService(BookingManagementCmsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DriverAndVehicle>> GetAllAsync()
        {
            return await _context.DriverAndVehicles.ToListAsync();
        }

        public async Task<DriverAndVehicle?> GetByIdAsync(int id)
        {
            return await _context.DriverAndVehicles.FindAsync(id);
        }

        public async Task<IEnumerable<DriverAndVehicle>> GetPagingAsync(int pageNumber, int pageSize)
        {
            return await _context.DriverAndVehicles
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<DriverAndVehicle>> SearchAsync(string keyword)
        {
            return await _context.DriverAndVehicles
                .Where(d =>
                      (d.VehicleNumber != null && d.VehicleNumber.Contains(keyword)) ||
                      (d.DriverLicenceNumber != null && d.DriverLicenceNumber.Contains(keyword)))
                .ToListAsync();
        }

        public async Task CreateAsync(DriverAndVehicle req)
        {
            _context.DriverAndVehicles.Add(req);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ApproveAsync(int id, bool isApproved)
        {
            var entity = await _context.DriverAndVehicles.FindAsync(id) ?? throw new KeyNotFoundException("Not found");
            entity.IsApprove = isApproved;
            entity.UpdatedOn = DateTime.UtcNow;
            _context.DriverAndVehicles.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateAsync(int id, DriverAndVehicle req)
        {
            var entity = await _context.DriverAndVehicles.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Not found");

            // Copy fields
            entity.DriverFirstName = req.DriverFirstName;
            entity.DriverLastName = req.DriverLastName;
            entity.VehicleType = req.VehicleType;
            entity.IsAvailable = req.IsAvailable;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.DriverAndVehicles.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DriverAndVehicles.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Not found");

            _context.DriverAndVehicles.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.DriverAndVehicles.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<DriverAndVehicle>> GetAvailableDriversAsync()
        {
            return await _context.DriverAndVehicles
                .Where(d => d.IsAvailable)
                .ToListAsync();
        }

        public async Task ToggleAvailabilityAsync(int id, bool status)
        {
            var entity = await _context.DriverAndVehicles.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Driver not found");

            entity.IsAvailable = status;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DriverAndVehicle>> FilterAsync(string vehicleType, bool? isAvailable)
        {
            var query = _context.DriverAndVehicles.AsQueryable();

            if (!string.IsNullOrEmpty(vehicleType))
                query = query.Where((d => d.VehicleType != null && d.VehicleType.Equals(vehicleType)));

            if (isAvailable.HasValue)
                query = query.Where(d => d.IsAvailable == isAvailable.Value);

            return await query.ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.DriverAndVehicles.CountAsync();
        }

        public IQueryable<DriverAndVehicle> GetAllQuery()
        {
            return _context.DriverAndVehicles.AsQueryable();
        }
    }

}

using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly BookingCmsContext _context;
        public DriverRepository(BookingCmsContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get All driver details
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            var orderEntities = await _context.Drivers.ToListAsync();
            // Map from EF (DA) entity to Domain entity
            return orderEntities.Select(e => new Driver
            {
                Id = e.Id,
                Address = e.Address,
                FirstName = e.FirstName,
                LastName = e.LastName,
                AboutOn = e.AboutOn,
                AvailabilityStatus = e.AvailabilityStatus,
                CreatedAt = e.CreatedAt,
                CreatedBy = e.CreatedBy,
                Email = e.Email,
                LicenseNumber = e.LicenseNumber,
                PhoneNumber = e.PhoneNumber,
                Photo = e.Photo,
                TenantId = e.TenantId,
                UpdatedAt = e.UpdatedAt,
                UpdatedBy = e.UpdatedBy,
            }).AsParallel();
        }

        public async Task<IEnumerable<DriverVehicle>> GetDriverVehicleList(int pageIndex, int pageSize, string searhKey = "")
        {
            return await _context.DriverVehicleMappings.AsNoTracking().Include(x => x.Vehicle).ThenInclude(x => x.VehicleMedia)

                .Include(x => x.Driver)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                  .Select(x => new DriverVehicle()
                  {
                      DriverId = x.DriverId,
                      VehicleId = x.VehicleId,
                      DriverName = $"{x.Driver.FirstName} {x.Driver.LastName}",
                      SeatingCapacity = x.Vehicle.SeatingCapacity ?? 2,
                      VehicleName = x.Vehicle.VehicleName,
                      VehicleThumbnail = x.Vehicle.VehicleMedia.FirstOrDefault(y => y.IsDefault).ThumbnailUrl ?? string.Empty,
                      VehicleType = x.Vehicle.VehicleTypeId.ToString()
                  }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        }
    }
}
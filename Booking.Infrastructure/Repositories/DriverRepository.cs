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
    public class DriverRepository(BookingCmsContext context) : IDriverRepository
    {
        private readonly BookingCmsContext _context = context;

        public async Task<int> ApproveDriverAsync(int DriverId)
        {
            var affected = await _context.Drivers
                              .Where(d => d.Id == DriverId)
                              .ExecuteUpdateAsync(setters => setters
                                  .SetProperty(d => d.ApproveDriver, true)
                              );
            return affected;
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
                Email = e.Email,
                LicenseNumber = e.LicenseNumber,
                PhoneNumber = e.PhoneNumber,
                Photo = e.Photo,
                TenantId = e.TenantId,
            }).AsParallel();
        }

        public async Task<IEnumerable<DriverVehicle>> GetDriverVehicleList(int pageIndex, int pageSize, string searchKey = "")
        {
            var DriverVehicleList = await _context.DriverVehicleMappings.AsNoTracking()
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.VehicleMedia)
                .Include(x => x.Driver)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new
                {
                    x.Vehicle,
                    x.Driver,
                    VehicleMedia = x.Vehicle.VehicleMedia.FirstOrDefault(x => x.IsDefault)
                }).ToListAsync();

            return DriverVehicleList
                .Select(x => new DriverVehicle()
                {
                    Driver = new Driver()
                    {
                        Id = x.Driver.Id,
                        FirstName = x.Driver.FirstName,
                        LastName = x.Driver.LastName,
                        Email = x.Driver.Email,
                        PhoneNumber = x.Driver.PhoneNumber,
                        Photo = x.Driver.Photo,
                        Address = x.Driver.Address,
                        LicenseNumber = x.Driver.LicenseNumber,
                        AboutOn = x.Driver.AboutOn,
                        AvailabilityStatus = x.Driver.AvailabilityStatus,
                    },
                    Vehicle = new Vehicle()
                    {
                        VehicleName = x.Vehicle.VehicleName,
                        Color = x.Vehicle.Color,
                        Description = x.Vehicle.Description,
                        AboutOnVehicle = x.Vehicle.AboutOnVehicle,
                        Id = x.Vehicle.Id,
                        Features = x.Vehicle.Features,
                        Make = x.Vehicle.Make,
                        VehicleNumber = x.Vehicle.VehicleNumber,
                        SeatingCapacity = x.Vehicle.SeatingCapacity,
                        Model = x.Vehicle.Model,
                        VehicleTypeId = x.Vehicle.VehicleTypeId,
                    },
                    VehicleMedia = new VehicleMedia()
                    {
                        MediaName = x.VehicleMedia?.MediaName ?? string.Empty,
                        MediaType = x.VehicleMedia?.MediaType ?? string.Empty,
                        MediaUrl = x.VehicleMedia?.MediaUrl ?? string.Empty,
                        ThumbnailUrl = x.VehicleMedia?.ThumbnailUrl ?? string.Empty,
                    }

                }).AsParallel().ToList();
        }

        public async Task<IEnumerable<VehicleMedia>> GetVehicleMediaList(int vehicleId)
        {
            var vehicleList = await _context.VehicleMedia.Where(x => x.Equals(vehicleId)).ToListAsync();
            return vehicleList.Select(x => new VehicleMedia()
            {
                MediaName = x.MediaName,
                MediaType = x.MediaType,
                IsDefault = x.IsDefault,
                MediaUrl = x.MediaUrl,
                ThumbnailUrl = x.ThumbnailUrl,
                VehicleId = x.VehicleId,
                Id = x.Id
            }).AsParallel().ToList();
        }
    }
}
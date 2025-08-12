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

        public async Task<DriverVehicleDTable> GetDriverVehicleList(int Skip, int Take, string searchKey = "")
        {
            //    var baseQuery = _context.DriverVehicleMappings
            //.AsNoTracking()
            //.Where(mapping =>
            //    string.IsNullOrEmpty(searchKey) ||
            //    mapping.Driver.FirstName.ToLower().Contains(searchKey) ||
            //    mapping.Driver.LastName.ToLower().Contains(searchKey) ||
            //    mapping.Driver.Email.ToLower().Contains(searchKey) ||
            //    mapping.Driver.PhoneNumber.ToLower().Contains(searchKey) ||
            //    mapping.Vehicle.VehicleName.ToLower().Contains(searchKey) ||
            //    mapping.Vehicle.VehicleNumber.ToLower().Contains(searchKey) ||
            //    mapping.Vehicle.Model.ToLower().Contains(searchKey)
            //)
            // Get total count for pagination
            var totalCount = await _context.DriverVehicleMappings.AsNoTracking().CountAsync();
            // Get paginated result with selected fields only
            var driverVehicleList = await _context.DriverVehicleMappings
                .AsNoTracking()
                .Select(mapping => new
                {
                    Driver = new
                    {
                        mapping.Driver.Id,
                        mapping.Driver.FirstName,
                        mapping.Driver.LastName,
                        mapping.Driver.Email,
                        mapping.Driver.PhoneNumber,
                        mapping.Driver.Photo,
                        mapping.Driver.Address,
                        mapping.Driver.LicenseNumber,
                        mapping.Driver.AboutOn,
                        mapping.Driver.AvailabilityStatus,
                        CreatedAt = mapping.Driver.CreatedAt
                    },
                    Vehicle = new
                    {
                        mapping.Vehicle.Id,
                        mapping.Vehicle.VehicleName,
                        mapping.Vehicle.Color,
                        mapping.Vehicle.Description,
                        mapping.Vehicle.AboutOnVehicle,
                        mapping.Vehicle.Features,
                        mapping.Vehicle.Make,
                        mapping.Vehicle.VehicleNumber,
                        mapping.Vehicle.SeatingCapacity,
                        mapping.Vehicle.Model,
                        mapping.Vehicle.VehicleTypeId,
                        DefaultMedia = mapping.Vehicle.VehicleMedia
                            .Where(m => m.IsDefault)
                            .Select(m => new
                            {
                                m.MediaName,
                                m.MediaType,
                                m.MediaUrl,
                                m.ThumbnailUrl
                            })
                            .FirstOrDefault()
                    }
                })
                .Skip(Skip)
                .Take(Take)
                .ToListAsync();

            // Final transformation to your actual models
            var resultList = driverVehicleList.Select(x => new DriverVehicle
            {
                Driver = new Driver
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
                    Created = x.Driver.CreatedAt
                },
                Vehicle = new Vehicle
                {
                    Id = x.Vehicle.Id,
                    VehicleName = x.Vehicle.VehicleName,
                    Color = x.Vehicle.Color,
                    Description = x.Vehicle.Description,
                    AboutOnVehicle = x.Vehicle.AboutOnVehicle,
                    Features = x.Vehicle.Features,
                    Make = x.Vehicle.Make,
                    VehicleNumber = x.Vehicle.VehicleNumber,
                    SeatingCapacity = x.Vehicle.SeatingCapacity,
                    Model = x.Vehicle.Model,
                    VehicleTypeId = x.Vehicle.VehicleTypeId
                },
                VehicleMedia = new VehicleMedia
                {
                    MediaName = x.Vehicle.DefaultMedia?.MediaName ?? string.Empty,
                    MediaType = x.Vehicle.DefaultMedia?.MediaType ?? string.Empty,
                    MediaUrl = x.Vehicle.DefaultMedia?.MediaUrl ?? string.Empty,
                    ThumbnailUrl = x.Vehicle.DefaultMedia?.ThumbnailUrl ?? string.Empty
                }
            }).ToList();

            // Final DTO return
            return new DriverVehicleDTable
            {
                Total = totalCount,
                Filtered = totalCount, // update if using filters
                driverVehicle = resultList
            };

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
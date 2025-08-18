using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class DriverRepository(BookingCmsContext context, IMemoryCache cache) : IDriverRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IMemoryCache _cache = cache;
        public async Task<int> ApproveDriverAsync(int DriverId)
        {
            var affected = await _context.Drivers
                              .Where(d => d.Id == DriverId)
                              .ExecuteUpdateAsync(setters => setters
                                  .SetProperty(d => d.ApproveDriver, true)
                              );
            return affected;
        }
        public async Task<int> RejectDriverAsync(int DriverId)
        {
            var affected = await _context.Drivers
                              .Where(d => d.Id == DriverId)
                              .ExecuteUpdateAsync(setters => setters
                                  .SetProperty(d => d.ApproveDriver, false)
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
        public async Task<IEnumerable<DriverVehicleExportEntity>> ExportAllAsync()
        {
            var DriverMappingData = await _context.DriverVehicleMappings
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
                            mapping.Driver.Address,
                            mapping.Driver.LicenseNumber,
                            mapping.Driver.AboutOn,
                            mapping.Driver.AvailabilityStatus,
                            mapping.Driver.ApproveDriver
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
                        }
                    }).ToListAsync();
            return DriverMappingData.Select(x => new DriverVehicleExportEntity()
            {
                FirstName = x.Driver.FirstName,
                LastName = x.Driver.LastName,
                Email = x.Driver.Email,
                PhoneNumber = x.Driver.PhoneNumber,
                Address = x.Driver.Address,
                LicenseNumber = x.Driver.LicenseNumber,
                AboutOn = x.Driver.AboutOn,
                AvailabilityStatus = x.Driver.AvailabilityStatus,
                ApproveDriver = x.Driver.ApproveDriver,
                VehicleName = x.Vehicle.VehicleName,
                AboutOnVehicle = x.Vehicle.AboutOnVehicle,
                VehicleNumber = x.Vehicle.VehicleNumber,
                SeatingCapacity = x.Vehicle.SeatingCapacity,
                Color = x.Vehicle.Color,
                Description = x.Vehicle.Description,
                Make = x.Vehicle.Make,
                Features = x.Vehicle.Features,
                Model = x.Vehicle.Model,
            }).AsParallel();
        }

        public async Task<DriverVehicleDTable> GetDriverVehicleList(int Skip, int Take, string searchKey = "")
        {
            if (!_cache.TryGetValue("drivervehiclelist", out var driverListing))
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
                            mapping.Driver.ApproveDriver,
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
                        Created = x.Driver.CreatedAt,
                        IsApproved = x.Driver.ApproveDriver

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
                driverListing = new DriverVehicleDTable
                {
                    Total = totalCount,
                    Filtered = totalCount, // update if using filters
                    DriverVehicle = resultList
                };
                _cache.Set("drivervehiclelist", driverListing, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20),
                });
            }
            return driverListing as DriverVehicleDTable ?? new DriverVehicleDTable();
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
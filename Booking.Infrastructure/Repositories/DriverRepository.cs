using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class DriverRepository(BookingCmsContext context, IMemoryCache cache) : IDriverRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IMemoryCache _cache = cache;
        public async Task<DriverTableEntity> GetDriverListAsync(string SearchValue, int Take, int Skip, CancellationToken token)
        {
            var q = _context.Drivers.AsNoTracking();
            var total = await q.CountAsync(token);

            if (!string.IsNullOrWhiteSpace(SearchValue))
            {
                q = q.Where(d => d.FirstName.Contains(SearchValue) || d.PhoneNumber.Contains(SearchValue));
            }
            // simple order by FullName default
            q = q.OrderByDescending(d => d.CreatedOn);

            var filtered = await q.CountAsync(token);
            var page = await q.Skip(Skip).Take(Take).ToListAsync(token);

            var response = new DriverTableEntity
            {
                TotalRecords = total,
                FilterRecords = filtered,
                DriverEntities = [.. page.Select(d => new DriverEntity
                {
                    Id = d.DriverId,
                    FirstName = d.FirstName,
                    PhoneNumber = d.PhoneNumber,
                    Email = d.Email,
                    LastName = d.LastName,
                    AboutOn = d.AboutOn,
                    AvailabilityStatus = d.AvailabilityStatus,
                    Address = d.Address,
                    Created=d.CreatedOn,
                    LicenseNumber = d.LicenseNumber,
                    Photo=d.Photo,
                    IsApproved=d.ApproveDriver??false,
                })]
            };
            return response;
        }
        public async Task<int> ApproveDriverAsync(int DriverId, CancellationToken token)
        {
            var approvedCount = await _context.Drivers
                             .Where(x => x.DriverId == DriverId)
                             .ExecuteUpdateAsync(setters => setters.SetProperty(d => d.ApproveDriver, true)
                             , cancellationToken: token);
            return approvedCount;
        }
        public async Task<int> RejectDriverAsync(int DriverId, CancellationToken token)
        {
            var rejectedCount = await _context.Drivers
                              .Where(d => d.DriverId == DriverId)
                              .ExecuteUpdateAsync(setters => setters
                                  .SetProperty(d => d.ApproveDriver, false)
                               , cancellationToken: token);
            return rejectedCount;
        }
        public async Task<int> ApproveDriversAsync(List<int> DriverIds, CancellationToken token)
        {
            var approvedCount = await _context.Drivers
                 .Where(d => DriverIds.Contains(d.DriverId))
                 .ExecuteUpdateAsync(setters => setters
                     .SetProperty(d => d.ApproveDriver, true),
                 cancellationToken: token);
            return approvedCount;
        }
        public async Task<int> RejectDriversAsync(List<int> DriverIds, CancellationToken token)
        {
            var rejectedCount = await _context.Drivers
                              .Where(d => DriverIds.Contains(d.DriverId))
                              .ExecuteUpdateAsync(setters => setters
                                  .SetProperty(d => d.ApproveDriver, false)
                               , cancellationToken: token);
            return rejectedCount;
        }
        public async Task<int> AssignVehicleAsync(int DriverId, int VehicleId, CancellationToken token)
        {
            if (!await _context.DriverVehicles.AnyAsync(x => x.DriverId == DriverId, cancellationToken: token))
            {
                // For async code, prefer AddAsync (especially if working with async value generation)
                await _context.DriverVehicles.AddAsync(new Data.Models.DriverVehicle()
                {
                    DriverId = DriverId,
                    VehicleId = VehicleId,

                }, token);
                return await _context.SaveChangesAsync(token);
            }
            return 0;
        }
        public async Task<DriverEntity?> GetDriverAsync(int DriverId, CancellationToken token)
        {
            return await _context.Drivers.Where(x => x.DriverId == DriverId)
                  .Select(x => new DriverEntity()
                  {
                      Id = x.DriverId,
                      AboutOn = x.AboutOn,
                      Address = x.Address,
                      AvailabilityStatus = x.AvailabilityStatus,
                      Email = x.Email,
                      FirstName = x.FirstName,
                      LastName = x.LastName,
                      Photo = x.Photo,
                      PhoneNumber = x.PhoneNumber,
                      LicenseNumber = x.LicenseNumber,
                      Created = x.CreatedOn,
                      IsApproved = x.ApproveDriver,
                  }).FirstOrDefaultAsync(token);
        }
    }
}


///// <summary>
///// Get All driver details
///// </summary>
///// <returns></returns>
//public async Task<IEnumerable<DriverEntity>> GetAllAsync()
//{
//    var orderEntities = await _context.Drivers.ToListAsync();
//    // Map from EF (DA) entity to Domain entity
//    return orderEntities.Select(e => new DriverEntity
//    {
//        Id = e.DriverId,
//        Address = e.Address,
//        FirstName = e.FirstName,
//        LastName = e.LastName,
//        AboutOn = e.AboutOn,
//        AvailabilityStatus = e.AvailabilityStatus,
//        Email = e.Email,
//        LicenseNumber = e.LicenseNumber,
//        PhoneNumber = e.PhoneNumber,
//        Photo = e.Photo,
//        TenantId = e.TenantId,
//    }).AsParallel();
//}
//public async Task<IEnumerable<DriverVehicleExportEntity>> ExportAllAsync()
//{
//    var DriverMappingData = await _context.DriverVehicles
//            .AsNoTracking()
//            .Select(mapping => new
//            {
//                Driver = new
//                {
//                    mapping.Driver.DriverId,
//                    mapping.Driver.FirstName,
//                    mapping.Driver.LastName,
//                    mapping.Driver.Email,
//                    mapping.Driver.PhoneNumber,
//                    mapping.Driver.Address,
//                    mapping.Driver.LicenseNumber,
//                    mapping.Driver.AboutOn,
//                    mapping.Driver.AvailabilityStatus,
//                    mapping.Driver.ApproveDriver
//                },
//                Vehicle = new
//                {
//                    mapping.Vehicle.VehicleId,
//                    mapping.Vehicle.Color,
//                    mapping.Vehicle.OtherInformation,
//                    mapping.Vehicle.AboutOnVehicle,
//                    mapping.Vehicle.Make,
//                    mapping.Vehicle.VehicleNumber,
//                    mapping.Vehicle.ModelName,
//                    mapping.Vehicle.BasePrice
//                }
//            }).ToListAsync();
//    return DriverMappingData.Select(x => new DriverVehicleExportEntity()
//    {
//        FirstName = x.Driver.FirstName,
//        LastName = x.Driver.LastName,
//        Email = x.Driver.Email,
//        PhoneNumber = x.Driver.PhoneNumber,
//        Address = x.Driver.Address,
//        LicenseNumber = x.Driver.LicenseNumber,
//        AboutOn = x.Driver.AboutOn,
//        AvailabilityStatus = x.Driver.AvailabilityStatus,
//        ApproveDriver = x.Driver.ApproveDriver,
//        AboutOnVehicle = x.Vehicle.AboutOnVehicle,
//        VehicleNumber = x.Vehicle.VehicleNumber,
//        Color = x.Vehicle.Color,
//        Description = x.Vehicle.OtherInformation,
//        Make = x.Vehicle.Make,
//        Model = x.Vehicle.ModelName,
//    }).AsParallel();
//}

//public async Task<DriverVehicleDTable> GetDriverVehicleList(int Skip, int Take, string searchKey = "")
//{
//    var cacheKey = $"drivervehiclelist_{Skip}_{Take}";
//    if (!_cache.TryGetValue(cacheKey, out var driverListing))
//    {
//        //    var baseQuery = _context.DriverVehicleMappings
//        //.AsNoTracking()
//        //.Where(mapping =>
//        //    string.IsNullOrEmpty(searchKey) ||
//        //    mapping.Driver.FirstName.ToLower().Contains(searchKey) ||
//        //    mapping.Driver.LastName.ToLower().Contains(searchKey) ||
//        //    mapping.Driver.Email.ToLower().Contains(searchKey) ||
//        //    mapping.Driver.PhoneNumber.ToLower().Contains(searchKey) ||
//        //    mapping.Vehicle.VehicleName.ToLower().Contains(searchKey) ||
//        //    mapping.Vehicle.VehicleNumber.ToLower().Contains(searchKey) ||
//        //    mapping.Vehicle.Model.ToLower().Contains(searchKey)
//        //)
//        // Get total count for pagination
//        var totalCount = await _context.DriverVehicles.AsNoTracking().CountAsync();
//        // Get paginated result with selected fields only
//        var driverVehicleList = await _context.DriverVehicles
//            .AsNoTracking()
//            .Select(mapping => new
//            {
//                Driver = new
//                {
//                    mapping.Driver.DriverId,
//                    mapping.Driver.FirstName,
//                    mapping.Driver.LastName,
//                    mapping.Driver.Email,
//                    mapping.Driver.PhoneNumber,
//                    mapping.Driver.Photo,
//                    mapping.Driver.Address,
//                    mapping.Driver.LicenseNumber,
//                    mapping.Driver.AboutOn,
//                    mapping.Driver.AvailabilityStatus,
//                    mapping.Driver.ApproveDriver,
//                    CreatedAt = mapping.Driver.CreatedOn
//                },
//                Vehicle = new
//                {
//                    mapping.Vehicle.VehicleId,
//                    mapping.Vehicle.Color,
//                    mapping.Vehicle.OtherInformation,
//                    mapping.Vehicle.AboutOnVehicle,
//                    mapping.Vehicle.Make,
//                    mapping.Vehicle.VehicleNumber,
//                    mapping.Vehicle.ModelName,
//                    DefaultMedia = mapping.Vehicle.DefaultImage
//                }
//            })
//            .Skip(Skip)
//            .Take(Take)
//            .ToListAsync();

//        // Final transformation to your actual models
//        var resultList = driverVehicleList.Select(x => new DriverVehicle
//        {
//            Driver = new DriverEntity
//            {
//                Id = x.Driver.DriverId,
//                FirstName = x.Driver.FirstName,
//                LastName = x.Driver.LastName,
//                Email = x.Driver.Email,
//                PhoneNumber = x.Driver.PhoneNumber,
//                Photo = x.Driver.Photo,
//                Address = x.Driver.Address,
//                LicenseNumber = x.Driver.LicenseNumber,
//                AboutOn = x.Driver.AboutOn,
//                AvailabilityStatus = x.Driver.AvailabilityStatus,
//                Created = x.Driver.CreatedAt,
//                IsApproved = x.Driver.ApproveDriver
//            },
//            Vehicle = new Vehicle
//            {
//                Id = x.Vehicle.VehicleId,
//                Color = x.Vehicle.Color,
//                Description = x.Vehicle.OtherInformation,
//                AboutOnVehicle = x.Vehicle.AboutOnVehicle,
//                Make = x.Vehicle.Make,
//                VehicleNumber = x.Vehicle.VehicleNumber,
//                Model = x.Vehicle.ModelName,

//            }
//        }).ToList();

//        // Final DTO return
//        driverListing = new DriverVehicleDTable
//        {
//            Total = totalCount,
//            Filtered = totalCount, // update if using filters
//            DriverVehicle = resultList
//        };
//        _cache.Set(cacheKey, driverListing, new MemoryCacheEntryOptions
//        {
//            SlidingExpiration = TimeSpan.FromMinutes(2),
//            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20),
//        });
//    }
//    return driverListing as DriverVehicleDTable ?? new DriverVehicleDTable();
//}

//public async Task<IEnumerable<VehicleMedia>> GetVehicleMediaList(int vehicleId)
//{
//    var vehicleList = await _context.VehicleMediaMappings.Where(x => x.Equals(vehicleId))
//        .Select(x => x.Media).ToListAsync();
//    return vehicleList.Select(x => new VehicleMedia()
//    {
//        MediaName = x.MediaName,
//        MediaType = x.MediaType,
//        MediaUrl = x.MediaUrl,
//        ThumbnailUrl = x.ThumbnailUrl,
//        VehicleId = vehicleId,
//    }).AsParallel().ToList();
//}

//public async Task<DriverVehicle?> GetDriverVehicle(int DriverId, int VehicleId)
//{
//    var driverVehicle = await _context.DriverVehicles
//          .AsNoTracking()
//          .Where(mapping => mapping.DriverId == DriverId && mapping.VehicleId == VehicleId)
//          .Select(mapping => new
//          {
//              Driver = new
//              {
//                  mapping.Driver.DriverId,
//                  mapping.Driver.FirstName,
//                  mapping.Driver.LastName,
//                  mapping.Driver.Email,
//                  mapping.Driver.PhoneNumber,
//                  mapping.Driver.Photo,
//                  mapping.Driver.Address,
//                  mapping.Driver.LicenseNumber,
//                  mapping.Driver.AboutOn,
//                  mapping.Driver.AvailabilityStatus,
//                  mapping.Driver.ApproveDriver,
//                  mapping.Driver.CreatedOn
//              },
//              Vehicle = new
//              {
//                  mapping.Vehicle.VehicleId,
//                  mapping.Vehicle.Color,
//                  mapping.Vehicle.OtherInformation,
//                  mapping.Vehicle.AboutOnVehicle,
//                  mapping.Vehicle.Make,
//                  mapping.Vehicle.VehicleNumber,
//                  mapping.Vehicle.ModelName,
//                  DefaultMedia = mapping.Vehicle.DefaultImage
//              }
//          }).FirstOrDefaultAsync();

//    // Final transformation to your actual models
//    return driverVehicle == null ? null :
//         new DriverVehicle
//         {
//             Driver = driverVehicle.Driver == null ? null :
//                     new DriverEntity
//                     {
//                         Id = driverVehicle.Driver.DriverId,
//                         FirstName = driverVehicle.Driver.FirstName,
//                         LastName = driverVehicle.Driver.LastName,
//                         Email = driverVehicle.Driver.Email,
//                         PhoneNumber = driverVehicle.Driver.PhoneNumber,
//                         Photo = driverVehicle.Driver.Photo,
//                         Address = driverVehicle.Driver.Address,
//                         LicenseNumber = driverVehicle.Driver.LicenseNumber,
//                         AboutOn = driverVehicle.Driver.AboutOn,
//                         AvailabilityStatus = driverVehicle.Driver.AvailabilityStatus,
//                         Created = driverVehicle.Driver.CreatedOn,
//                         IsApproved = driverVehicle.Driver.ApproveDriver
//                     },
//             Vehicle = driverVehicle.Vehicle == null ? null :
//             new Vehicle
//             {
//                 Id = driverVehicle.Vehicle.VehicleId,
//                 Color = driverVehicle.Vehicle.Color,
//                 Description = driverVehicle.Vehicle.OtherInformation,
//                 AboutOnVehicle = driverVehicle.Vehicle.AboutOnVehicle,
//                 Make = driverVehicle.Vehicle.Make,
//                 VehicleNumber = driverVehicle.Vehicle.VehicleNumber,
//                 Model = driverVehicle.Vehicle.ModelName,
//             },
//         };
//}

//public Task<DriverVehicle?> GetDriverVehicle(int DriverVehileId)
//{
//    throw new NotImplementedException();
//}
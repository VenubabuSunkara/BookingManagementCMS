using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json;

namespace Booking.Infrastructure.Repositories
{
    public class DriverVehicleRepository(BookingCmsContext context, IMemoryCache cache) : IDriverVehicleRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IMemoryCache _cache = cache;
        private IQueryable<DriverVehicleFullEntity> GetDriverVehicleFullQuery()
        {
            return _context.DriverVehicles
                             .Where(dv => dv.Driver.AvailabilityStatus == true && dv.Driver.IsActive == true)
                             .Select(dv => new DriverVehicleFullEntity
                             {
                                 Driver = new DriverEntity
                                 {
                                     Id = dv.Driver.DriverId,
                                     FirstName = dv.Driver.FirstName,
                                     LastName = dv.Driver.LastName,
                                     Address = dv.Driver.Address,
                                     Email = dv.Driver.Email,
                                     LicenseNumber = dv.Driver.LicenseNumber,
                                     PhoneNumber = dv.Driver.PhoneNumber,
                                     Photo = dv.Driver.Photo,
                                     AboutOn = dv.Driver.AboutOn,
                                     AvailabilityStatus = dv.Driver.AvailabilityStatus
                                 },
                                 Vehicle = new VehicleEntity
                                 {
                                     Id = dv.Vehicle.VehicleId,
                                     ModelName = dv.Vehicle.Model,
                                     VehicleNumber = dv.Vehicle.VehicleNumber,
                                     DefaultImage = dv.Vehicle.DefaultImage,
                                     BasePrice = dv.Vehicle.Fare,
                                     AboutOnVehicle = dv.Vehicle.AboutOnVehicle,
                                     Color = dv.Vehicle.Color,
                                 },
                                 VehicleMedia = dv.Vehicle.VehicleMediaMappings.Where(x => x.VehicleId == dv.VehicleId)
                                     .Select(m => new VehicleMedia
                                     {
                                         Id = m.Media.MediaId,
                                         ThumbnailUrl = m.Media.ThumbnailUrl,
                                         MediaName = m.Media.MediaName,
                                         MediaType = m.Media.MediaType,
                                         MediaUrl = m.Media.MediaUrl,
                                     }).ToList(),
                                 DriverVehicleAvailabilityEntities = dv.Driver.DriverVehicleAvailabilities
                                     .Select(a => new DriverVehicleAvailabilityEntity
                                     {
                                         DriverId = a.DriverId,
                                         VehicleId = a.VehicleId,
                                         AvailableFrom = a.AvailableFrom,
                                         AvailabilityId = a.AvailabilityId,
                                         SlotEnd = a.SlotEnd,
                                         SlotStart = a.SlotStart,

                                     }).ToList(),

                                 FeatureEntities = dv.Vehicle.VehicleFeatureMappings
                                     .Select(f => new FeatureEntity
                                     {
                                         FeatureId = f.Feature.FeatureId,
                                         FeatureName = f.Feature.FeatureName,
                                         FeatureType = f.Feature.FeatureType,
                                         FeatureValue = f.Feature.FeatureValue,
                                         VehicleId = f.VehicleId,
                                     }).ToList(),
                                 BookingOrdersEntities = dv.Driver.BookingOrders
                                     .Select(b => new BookingOrderEntity
                                     {
                                         BookingNumber = b.BookingNumber,
                                         CustomerId = b.CustomerId,
                                         DropLocation = b.DropLocation,
                                         BookingDate = b.BookingDate,
                                         ScheduledDropTime = b.ScheduledDropTime,
                                         DriverId = b.DriverId,
                                         ActualFare = b.ActualFare,
                                         EstimatedFare = b.EstimatedFare,
                                         PickupLocation = b.PickupLocation,
                                         ScheduledPickupTime = b.ScheduledPickupTime,
                                         TripType = b.TripType,
                                         PaymentStatus = b.PaymentStatus,
                                         BookingOrderId = b.BookingOrderId,
                                         VehicleId = b.VehicleId,
                                         Status = b.Status
                                     }).ToList(),
                                 PaymentEntities = dv.Vehicle.BookingOrders.Select(x => x.Payments.FirstOrDefault(y => y.Status == "Completed"))
                                     .Select(p => new PaymentEntity
                                     {
                                         Amount = p.Amount,
                                         PaymentMode = p.PaymentMode,
                                         PaymentDate = p.PaymentDate,
                                         PaymentId = p.PaymentId
                                     }).ToList()
                             }).AsNoTracking()
                             .AsQueryable();
        }
        private IQueryable<DriverVehicleEntity> GetDriverVehicleTableQuery()
        {
            return _context.DriverVehicles.Where(dv => dv.Driver.AvailabilityStatus == true
                                    && dv.Driver.IsActive == true)
                             .Select(dv => new DriverVehicleEntity
                             {
                                 Driver = new DriverEntity
                                 {
                                     Id = dv.Driver.DriverId,
                                     FirstName = dv.Driver.FirstName,
                                     LastName = dv.Driver.LastName,
                                     Address = dv.Driver.Address,
                                     Email = dv.Driver.Email,
                                     LicenseNumber = dv.Driver.LicenseNumber,
                                     PhoneNumber = dv.Driver.PhoneNumber,
                                     Photo = dv.Driver.Photo,
                                     AboutOn = dv.Driver.AboutOn,
                                     AvailabilityStatus = dv.Driver.AvailabilityStatus
                                 },
                                 Vehicle = new VehicleEntity
                                 {
                                     Id = dv.Vehicle.VehicleId,
                                     ModelName = dv.Vehicle.Model,
                                     VehicleNumber = dv.Vehicle.VehicleNumber,
                                     DefaultImage = dv.Vehicle.DefaultImage,
                                     BasePrice = dv.Vehicle.Fare,
                                     AboutOnVehicle = dv.Vehicle.AboutOnVehicle,
                                     Color = dv.Vehicle.Color,
                                 },
                             }).AsNoTracking()
                             .AsQueryable();
        }
        public async Task<DriverVehicleTableEntity> DriverVehicleList(string SearchValue, int Take, int Skip, CancellationToken token)
        {
            // Create a cache key that varies by search + page
            var cacheKey = $"DriverVehicle:{SearchValue}:{Skip}:{Take}";

            if (!_cache.TryGetValue(cacheKey, out DriverVehicleTableEntity? cached))
            {
                var driverVehicleQuery = GetDriverVehicleTableQuery();
                var total = await driverVehicleQuery.CountAsync(cancellationToken: token);
                var FilterRecords = total;
                if (!string.IsNullOrEmpty(SearchValue))
                {
                    var term = $"%{SearchValue}%";
                    driverVehicleQuery = driverVehicleQuery.Where(x =>
                        EF.Functions.Like(x.Driver.FirstName, term) ||
                        EF.Functions.Like(x.Driver.LastName, term) ||
                        EF.Functions.Like(x.Driver.LicenseNumber, term) ||
                        EF.Functions.Like(x.Vehicle.VehicleNumber, term));
                    FilterRecords = await driverVehicleQuery.CountAsync(cancellationToken: token);
                }
                var driverVehiclesList = await driverVehicleQuery.Skip(Skip).Take(Take).ToListAsync(cancellationToken: token);
                // Cache for 5 minutes (absolute)

                var tableResults = new DriverVehicleTableEntity()
                {
                    Total = total,
                    DriverVehicle = driverVehiclesList,
                    Filtered = FilterRecords
                };
                var cacheOptions = new MemoryCacheEntryOptions()
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _cache.Set(cacheKey, tableResults, cacheOptions);
                return tableResults;
            }
            return cached!;
        }
        public async Task<int> RejectDriverVehicleAsync(int DriverId, int VehicleId, CancellationToken token)
        {
            var rejectedCount = await _context.Drivers.Where(d => d.DriverId == DriverId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(d => d.IsActive, false), token);
            return rejectedCount;
        }

        private bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var hasher = new PasswordHasher<object>();
            var result = hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
        public async Task<int> CreateDriverVehicle(CreateDriverVehicleEntity entity, CancellationToken token)
        {
            var hasher = new PasswordHasher<object>();
            string hashed = hasher.HashPassword(null, entity.DriverEntity.Password);
            var driverTable = new Data.Models.Driver()
            {
                PhoneNumber = entity.DriverEntity.PhoneNumber,
                FirstName = entity.DriverEntity.FirstName,
                LastName = entity.DriverEntity.LastName,
                Email = entity.DriverEntity.Email,
                AboutOn = entity.DriverEntity.AboutOn,
                Address = entity.DriverEntity.Address,
                DateOfBirth = entity.DriverEntity.DateOfBirth,
                UpdatedOn = entity.DriverEntity.UpdatedOn,
                UpdatedBy = entity.DriverEntity.UpdatedBy,
                CreatedBy = entity.DriverEntity.CreatedBy,
                CreatedOn = entity.DriverEntity.CreatedOn,
                UserName = entity.DriverEntity.UserName,
                Gender = entity.DriverEntity.Gender,
                AvailabilityStatus = entity.DriverEntity.AvailabilityStatus,
                ApproveDriver = entity.DriverEntity.ApproveDriver,
                IsActive = entity.DriverEntity.IsActive,
                Photo = entity.DriverEntity.Photo,
                TenantId = entity.DriverEntity.TenantId,
                PasswordHash = hashed,
                LicenseNumber = entity.DriverEntity.LicenseNumber,
            };
            await _context.Drivers.AddAsync(driverTable, token);
            await _context.SaveChangesAsync(token);

            var vehicleTable = new Data.Models.Vehicle()
            {
                VehicleNumber = entity.VehicleEntity.VehicleNumber,
                Model = entity.VehicleEntity.Model,
                Color = entity.VehicleEntity.Color,
                Fare = entity.VehicleEntity.Fare,
                CarName = entity.VehicleEntity.CarName,
                AverageMileage = entity.VehicleEntity.AverageMileage,
                Fecility = entity.VehicleEntity.Fecility,
                PollucationCertificationNumber = entity.VehicleEntity.PollucationCertificationNumber,
                InsurenceValidUntil = entity.VehicleEntity.InsurenceValidUntil,
                InsurnceNumber = entity.VehicleEntity.InsurnceNumber,
                VehicleTypeId = entity.VehicleEntity.VehicleTypeId,
                AboutOnVehicle = entity.VehicleEntity.AboutOnVehicle,
                DefaultImage = entity.VehicleEntity.DefaultImage,
                UpdatedOn = entity.VehicleEntity.UpdatedOn,
                UpdatedBy = entity.VehicleEntity.UpdatedBy,
                CreatedBy = entity.VehicleEntity.CreatedBy,
                CreatedOn = entity.VehicleEntity.CreatedOn,
            };
            await _context.Vehicles.AddAsync(vehicleTable, token);
            await _context.SaveChangesAsync(token);
            await _context.DriverVehicles.AddAsync(new Data.Models.DriverVehicle()
            {
                DriverId = driverTable.DriverId,
                VehicleId = vehicleTable.VehicleId,
                CreatedBy = entity.DriverEntity.CreatedBy,
                CreatedOn = entity.DriverEntity.CreatedOn,
                UpdatedBy = entity.DriverEntity.UpdatedBy,
                UpdatedOn = entity.DriverEntity.UpdatedOn,
            }, token);
            var result = await _context.SaveChangesAsync(token);
            return result;
        }

        public Task<int> UpdateDriverVehicle(CreateDriverVehicleEntity entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateDriverVehicleEntity?> GetDriverVehicleById(int DriverId, int VehicleId, CancellationToken token)
        {
            CreateDriverVehicleEntity model = new()
            {
                DriverEntity = await _context.Drivers.AsNoTracking().Where(x => x.DriverId.Equals(DriverId))
                    .Select(entity => new CreateDriverEntity()
                    {
                        PhoneNumber = entity.PhoneNumber,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Email = entity.Email,
                        AboutOn = entity.AboutOn,
                        Address = entity.Address,
                        DateOfBirth = entity.DateOfBirth,
                        UpdatedOn = entity.UpdatedOn,
                        UpdatedBy = entity.UpdatedBy,
                        CreatedBy = entity.CreatedBy,
                        CreatedOn = entity.CreatedOn,
                        UserName = entity.UserName,
                        Gender = entity.Gender,
                        AvailabilityStatus = entity.AvailabilityStatus,
                        ApproveDriver = entity.ApproveDriver,
                        IsActive = entity.IsActive,
                        Photo = entity.Photo,
                        TenantId = entity.TenantId,
                        LicenseNumber = entity.LicenseNumber,
                    }).FirstOrDefaultAsync(cancellationToken: token) ?? new CreateDriverEntity()
                    ,
                VehicleEntity = await _context.Vehicles.AsNoTracking().Where(x => x.VehicleId.Equals(VehicleId))
                     .Select(entity => new CreateVehicleEntity()
                     {
                         VehicleNumber = entity.VehicleNumber,
                         Model = entity.Model,
                         Color = entity.Color,
                         Fare = entity.Fare,
                         CarName = entity.CarName,
                         AverageMileage = entity.AverageMileage,
                         Fecility = entity.Fecility,
                         PollucationCertificationNumber = entity.PollucationCertificationNumber,
                         InsurenceValidUntil = entity.InsurenceValidUntil,
                         InsurnceNumber = entity.InsurnceNumber,
                         VehicleTypeId = entity.VehicleTypeId,
                         AboutOnVehicle = entity.AboutOnVehicle,
                         DefaultImage = entity.DefaultImage,
                         UpdatedOn = entity.UpdatedOn,
                         UpdatedBy = entity.UpdatedBy,
                         CreatedBy = entity.CreatedBy,
                         CreatedOn = entity.CreatedOn,
                     }).FirstOrDefaultAsync(token) ?? new CreateVehicleEntity(),
                DriverPhotojson = JsonSerializer.Serialize(await _context.DriverMediaMappings.AsNoTracking().Where(x => x.DriverId == DriverId)
                .Select(x => new
                {
                    x.Media.MediaName,
                    x.Media.MediaType,
                    x.Media.ThumbnailUrl,
                    x.Media.MediaUrl
                }).ToArrayAsync(cancellationToken: token)),
                VehicleDefaultImagejson = JsonSerializer.Serialize(await _context.VehicleMediaMappings.AsNoTracking().Where(x => x.VehicleId == VehicleId)
                .Select(x => new
                {
                    x.Media.MediaName,
                    x.Media.MediaType,
                    x.Media.ThumbnailUrl,
                    x.Media.MediaUrl
                }).ToArrayAsync(cancellationToken: token)),
            };
            return model;
        }
    }
}

using Booking.Application.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Booking.Infrastructure.Repositories
{
    public class VehicleRepository(BookingCmsContext context, IMemoryCache cache) : IVehicleRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IMemoryCache _cache = cache;
        public async Task<VehicleTableEntity> GetVehicleListAsync(string SearchValue, int Take, int Skip, CancellationToken token)
        {
            var q = _context.Vehicles.AsNoTracking();
            var total = await q.CountAsync(token);

            if (!string.IsNullOrWhiteSpace(SearchValue))
            {
                q = q.Where(d => d.Model.Contains(SearchValue) || d.VehicleNumber.Contains(SearchValue));
            }
            // simple order by FullName default
            q = q.OrderByDescending(d => d.CreatedOn);

            var filtered = await q.CountAsync(token);
            var page = await q.Skip(Skip).Take(Take).ToListAsync(token);

            var response = new VehicleTableEntity
            {
                TotalRecords = total,
                FilterRecords = filtered,
                VehicleEntities = [.. page.Select(d => new VehicleEntity
                {
                    Id = d.VehicleId,
                    ModelName=d.Model,
                    VehicleNumber=d.VehicleNumber,
                    Color=d.Color,
                    AboutOnVehicle=d.AboutOnVehicle,
                    DefaultImage=d.DefaultImage,
                    CreatedOn=d.CreatedOn,
                    BasePrice=d.Fare
                })]
            };
            return response;
        }
        public async Task<int> ApproveVehicleAsync(int VehicleId, CancellationToken token)
        {
            var approvedCount = await _context.Vehicles
                             .Where(x => x.VehicleId == VehicleId)
                             .ExecuteUpdateAsync(setters => setters.SetProperty(d => d.VehicleId, 0)
                             , cancellationToken: token);
            return approvedCount;
        }
        public async Task<int> RejectVehicleAsync(int VehicleId, CancellationToken token)
        {
            var rejectedCount = await _context.Vehicles
                              .Where(d => d.VehicleId == VehicleId)
                              .ExecuteUpdateAsync(setters => setters
                                  .SetProperty(d => d.VehicleId, 0)
                               , cancellationToken: token);
            return rejectedCount;
        }
        public async Task<int> ApproveVehiclesAsync(List<int> VehicleIds, CancellationToken token)
        {
            var approvedCount = await _context.Vehicles
                 .Where(d => VehicleIds.Contains(d.VehicleId))
                 .ExecuteUpdateAsync(setters => setters
                     .SetProperty(d => d.VehicleId, 0),
                 cancellationToken: token);
            return approvedCount;
        }
        public async Task<int> RejectVehiclesAsync(List<int> VehicleIds, CancellationToken token)
        {
            var rejectedCount = await _context.Vehicles
                              .Where(d => VehicleIds.Contains(d.VehicleId))
                              .ExecuteUpdateAsync(setters => setters
                                  .SetProperty(d => d.VehicleId, 0)
                               , cancellationToken: token);
            return rejectedCount;
        }
        public async Task<int> AssignDriverAsync(int DriverId, int VehicleId, CancellationToken token)
        {
            if (!await _context.DriverVehicles.AnyAsync(x => x.VehicleId == VehicleId, cancellationToken: token))
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
        public async Task<VehicleEntity?> GetVehicleAsync(int VehicleId, CancellationToken token)
        {
            return await _context.Vehicles.Where(x => x.VehicleId == VehicleId)
                  .Select(d => new VehicleEntity()
                  {
                      Id = d.VehicleId,
                      ModelName = d.Model,
                      VehicleNumber = d.VehicleNumber,
                      Color = d.Color,
                      AboutOnVehicle = d.AboutOnVehicle,
                      DefaultImage = d.DefaultImage,
                      CreatedOn = d.CreatedOn,
                      BasePrice = d.Fare,
                      CreatedBy = d.CreatedBy,
                      UpdatedBy = d.UpdatedBy,
                      UpdatedOn = d.UpdatedOn,
                  }).FirstOrDefaultAsync(token);
        }

        public async Task<IEnumerable<UnAssignedVehiclesEntity>> GetUnAssignedVehiclesList(CancellationToken token)
        {
            return await _context.Vehicles.AsNoTracking()
                                   .Where(d => !_context.DriverVehicles.AsNoTracking()
                                         .Any(dv => dv.VehicleId == d.VehicleId))
                                   .Select(d => new UnAssignedVehiclesEntity()
                                   {
                                       Id = d.VehicleId,
                                       RegistrationNumber = d.VehicleNumber
                                   }).ToListAsync(token);
        }
        public async Task<bool> AssignDriver(AssignVehicleDriverEntity model, CancellationToken token)
        {
            await _context.DriverVehicles.AddAsync(new Data.Models.DriverVehicle()
            {
                DriverId = model.DriverId,
                VehicleId = model.VehicleId,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                CreatedBy = model.CreatedBy,
                UpdatedBy = model.CreatedBy
            }, token);
            var records = await _context.SaveChangesAsync(token);
            return records > 0;
        }
        public async Task<IEnumerable<VehicleDropdownEntity>> GetVehicleDropdownList(CancellationToken token)
        {
            return await _context.Vehicles.AsNoTracking()
                                  .Select(d => new VehicleDropdownEntity()
                                  {
                                      VehicleId = d.VehicleId,
                                      ModelName = d.Model,
                                      RegistrationNumber = d.VehicleNumber
                                  }).ToListAsync(token);
        }
    }
}
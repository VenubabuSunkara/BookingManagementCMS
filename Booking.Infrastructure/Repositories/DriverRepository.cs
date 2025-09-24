using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Booking.Infrastructure.Repositories
{
    public class DriverRepository(BookingCmsContext context, IMemoryCache cache) : IDriverRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IMemoryCache _cache = cache;
        public async Task<DriverTableEntity> GetDriverListAsync(string SearchValue, int Take, int Skip, CancellationToken token)
        {
            var q = _context.Drivers
                .Include(x => x.DriverVehicle).ThenInclude(y => y.Vehicle)
                .AsNoTracking();
            var total = await q.CountAsync(token);

            if (!string.IsNullOrWhiteSpace(SearchValue))
            {
                q = q.Where(d =>
                            d.FirstName.Contains(SearchValue) ||
                            d.PhoneNumber.Contains(SearchValue) ||
                            d.Email.Contains(SearchValue) ||
                            d.LastName.Contains(SearchValue) ||
                            d.LicenseNumber.Contains(SearchValue)
                );
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
                    IsVehicleAssigned=d.DriverVehicle?.Vehicle is not null
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
            await _context.DriverVehicles.Where(u => u.DriverId == DriverId).ExecuteDeleteAsync(token);
            var rejectedCount = await _context.Drivers.Where(d => d.DriverId == DriverId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(d => d.IsActive, false), token);
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
        public async Task<IEnumerable<DriverExportEntity>> ExportAllAsync(CancellationToken token)
        {
            return await _context.Drivers
                    .AsNoTracking()
                    .Select(x => new DriverExportEntity()
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        Address = x.Address,
                        LicenseNumber = x.LicenseNumber,
                        AboutOn = x.AboutOn,
                        AvailabilityStatus = x.AvailabilityStatus,
                        Id = x.DriverId,
                        IsApproved = x.ApproveDriver,
                    }).ToListAsync(token);
        }

        public async Task<IEnumerable<UnAssignedDriversEntity>> GetUnAssignedDriversList(CancellationToken token)
        {
            return await _context.Drivers.AsNoTracking()
                                  .Where(d => !_context.DriverVehicles.AsNoTracking()
                                        .Any(dv => dv.DriverId == d.DriverId))
                                  .Select(d => new UnAssignedDriversEntity()
                                  {
                                      Id = d.DriverId,
                                      License = d.LicenseNumber,
                                      FullName = $"{d.FirstName} {d.LastName}"
                                  }).ToListAsync(token);
        }

        public async Task<IEnumerable<DriversDropdownEntity>> GetDriversDropdownList(CancellationToken token)
        {
            return await _context.Drivers.AsNoTracking()
                                  .Select(d => new DriversDropdownEntity()
                                  {
                                      Id = d.DriverId,
                                      License = d.LicenseNumber,
                                      FullName = $"{d.FirstName} {d.LastName}"
                                  }).ToListAsync(token);
        }
    }
}
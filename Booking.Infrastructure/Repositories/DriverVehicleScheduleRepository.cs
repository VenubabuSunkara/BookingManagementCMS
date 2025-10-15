using Amazon.Runtime.Internal.Util;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class DriverVehicleScheduleRepository(BookingCmsContext context, IMemoryCache cache) : IDriverVehicleScheduleRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IMemoryCache _cache = cache;
        public Task<int> CreateDriverVehicleSchedleAsync(DriverVehicleAvailabilityEntity entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<DriverVehicleSchedulerTableEntity> DriverVehicleSchedulesList(string ScheduleSearch, int Take, int Skip, CancellationToken token)
        {
            var query = from dva in _context.DriverVehicleAvailabilities.AsNoTracking()
                        join d in _context.Drivers.AsNoTracking() on dva.DriverId equals d.DriverId
                        join v in _context.Vehicles.AsNoTracking() on dva.VehicleId equals v.VehicleId
                        select new DriverVehicleAvailabilityEntity
                        {
                            DriverId = dva.DriverId,
                            VehicleId = dva.VehicleId,
                            AvailableFrom = dva.AvailableFrom,
                            AvailableTo = dva.AvailableTo,
                            SlotStart = dva.SlotStart,
                            SlotEnd = dva.SlotEnd,
                            IsFullDay = dva.IsFullDay,
                            Driver = new CreateDriverEntity
                            {
                                FirstName = d.FirstName,
                                LastName = d.LastName,
                                LicenseNumber = d.LicenseNumber,
                            },
                            Vehicle = new CreateVehicleEntity
                            {
                                Model = v.Model,
                                VehicleNumber = v.VehicleNumber,
                                CarName = v.CarName,
                            },
                        };
            var totalRecords = await query.CountAsync(token);
            if (!string.IsNullOrEmpty(ScheduleSearch))
            {
                query = query.Where(x => x.Vehicle.CarName.Contains(ScheduleSearch) || x.Driver.FirstName.Contains(ScheduleSearch));
            }
            var filterRecords = await query.CountAsync(token);
            var data = await query.Skip(Skip).Take(Take).ToListAsync(token);
            return new DriverVehicleSchedulerTableEntity
            {
                TotalRecords = totalRecords,
                FilterRecords = filterRecords,
                AvailabilityEntities = data
            };
        }

        public async Task<List<DriverVehicleAvailabilityEntity>> GetDriverVehicleScheduleById(int DriverId, int VehicleId, CancellationToken token)
        {
            return await _context.DriverVehicleAvailabilities.AsNoTracking()
                .Where(x => x.DriverId == DriverId)
                .Select(x => new DriverVehicleAvailabilityEntity()
                {
                    DriverId = x.DriverId,
                    VehicleId = x.VehicleId,
                    AvailableFrom = x.AvailableFrom,
                    AvailableTo = x.AvailableTo,
                    SlotStart = x.SlotStart,
                    SlotEnd = x.SlotEnd,
                    IsFullDay = x.IsFullDay,
                    Driver = new CreateDriverEntity
                    {
                        FirstName = x.Driver.FirstName,
                        LastName = x.Driver.LastName,
                        LicenseNumber = x.Driver.LicenseNumber,
                    },
                    Vehicle = new CreateVehicleEntity
                    {
                        Model = x.Vehicle.Model,
                        VehicleNumber = x.Vehicle.VehicleNumber,
                        CarName = x.Vehicle.CarName,
                    },

                }).ToListAsync(token);
        }

        public Task<int> RejectDriverVehicleScheduleAsync(int DriverId, int VehicleId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateDriverVehicleScheduleAsync(DriverVehicleAvailabilityEntity entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}

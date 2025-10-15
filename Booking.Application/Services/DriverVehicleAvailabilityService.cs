using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class DriverVehicleAvailabilityService(IDriverVehicleScheduleRepository scheduleRepository) : IDriverVehicleAvailabilityService
    {
        private readonly IDriverVehicleScheduleRepository _scheduleRepository = scheduleRepository;
        public Task<int> CreateDriverVehicleSchedleAsync(DriverVehicleScheduleDto entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<DriverVehicleSchedulerTableDto> DriverVehicleSchedulesList(string ScheduleSearch, int Take, int Skip, CancellationToken token)
        {
            var schedules = await _scheduleRepository.DriverVehicleSchedulesList(ScheduleSearch, Take, Skip, token);
            return new DriverVehicleSchedulerTableDto()
            {
                TotalRecords = schedules.TotalRecords,
                FilterRecords = schedules.FilterRecords,
                DriverVehicleSchedules = [.. schedules.AvailabilityEntities.Select(d => new DriverVehicleScheduleDto()
                {
                    DriverId = d.DriverId,
                    VehicleId = d.VehicleId,
                    AvailableFrom = d.AvailableFrom,
                    AvailableTo = d.AvailableTo,
                    SlotStart = d.SlotStart,
                    SlotEnd = d.SlotEnd,
                    IsFullDay = d.IsFullDay,
                    Driver = new CreateDriverDto()
                    {
                        DriverId = d.DriverId,
                        FirstName = d.Driver.FirstName,
                        LastName = d.Driver.LastName,
                        LicenseNumber = d.Driver.LicenseNumber
                    },
                    Vehicle = new CreateVehicleDto()
                    {
                        VehicleId = d.VehicleId,
                        CarName = d.Vehicle.CarName,
                        Model = d.Vehicle.Model,
                        VehicleNumber = d.Vehicle.VehicleNumber
                    }
                })]
            };
        }

        public async Task<IEnumerable<DriverVehicleScheduleDto>> GetDriverVehicleScheduleById(int DriverId, int VehicleId, CancellationToken token)
        {
           var schedules = await _scheduleRepository.GetDriverVehicleScheduleById(DriverId, VehicleId, token);
              return [.. schedules.Select(d => new DriverVehicleScheduleDto()
              {
                DriverId = d.DriverId,
                VehicleId = d.VehicleId,
                AvailableFrom = d.AvailableFrom,
                AvailableTo = d.AvailableTo,
                SlotStart = d.SlotStart,
                SlotEnd = d.SlotEnd,
                IsFullDay = d.IsFullDay,
                Driver = new CreateDriverDto()
                {
                     DriverId = d.DriverId,
                     FirstName = d.Driver.FirstName,
                     LastName = d.Driver.LastName,
                     LicenseNumber = d.Driver.LicenseNumber
                },
                Vehicle = new CreateVehicleDto()
                {
                     VehicleId = d.VehicleId,
                     CarName = d.Vehicle.CarName,
                     Model = d.Vehicle.Model,
                     VehicleNumber = d.Vehicle.VehicleNumber
                }
              })];
        }

        public Task<int> RejectDriverVehicleScheduleAsync(int DriverId, int VehicleId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateDriverVehicleScheduleAsync(DriverVehicleScheduleDto entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}

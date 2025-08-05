using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class DriverService(IDriverRepository driverRepository) : IDriverService
    {
        private readonly IDriverRepository _driverRepository = driverRepository;

        public async Task<int> ApproveDriverAsync(int DriverId)
        {
            return await _driverRepository.ApproveDriverAsync(DriverId);
        }

        public async Task<IEnumerable<DriverDto>> GetAllAsync()
        {
            var drivers = await _driverRepository.GetAllAsync();
            return drivers.Select(d => new DriverDto
            {
                Id = d.Id,
                FullName = d.GetFullName(),
                PhoneNumber = d.PhoneNumber ?? string.Empty
            }).AsParallel();
        }

        public async Task<IEnumerable<DriverVehicleDto>> GetDriverVehicleList(int pageIndex, int pageSize, string searchKey = "")
        {
            var DriverVehicleList = await _driverRepository.GetDriverVehicleList(pageIndex, pageSize, searchKey);

            return DriverVehicleList.Select(d => new DriverVehicleDto
            {
                DriverId = d.Driver.Id,
                VehicleId = d.Vehicle.Id,
                DriverName = d.Driver.GetFullName(),
                DriverContact = d.Driver.PhoneNumber ?? string.Empty,
                VehicleName = d.Vehicle.VehicleName ?? "Unknown",
                SeatingCapacity = d.Vehicle.SeatingCapacity ?? 2,
                VehicleThumbnail = d.VehicleMedia.ThumbnailUrl ?? "Unknown",
                VehicleType = d.Vehicle.VehicleTypeId.ToString() ?? "Unknown",
                Created = d.Driver.Created ?? DateTime.Now

            }).AsParallel();

        }

        public async Task<IEnumerable<VehicleMediaDto>> GetVehicleMediaList(int vehicleId)
        {
            var VehicleMedia = await _driverRepository.GetVehicleMediaList(vehicleId);
            return VehicleMedia.Select(v => new VehicleMediaDto()
            {
                IsDefault = v.IsDefault,
                Id = v.Id,
                MediaName = v.MediaName ?? "Unknown",
                MediaType = v.MediaType ?? "Unknown",
                MediaUrl = v.MediaUrl ?? "Unknown",
                VehicleId = v.VehicleId,
                ThumbnailUrl = v.ThumbnailUrl ?? "Unknown",
            }).AsParallel();
        }
    }
}

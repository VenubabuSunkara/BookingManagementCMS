using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
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
        public async Task<int> RejectDriverAsync(int DriverId)
        {
            return await _driverRepository.RejectDriverAsync(DriverId);
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

        public async Task<DriverDataTableDto> GetDriverVehicleList(int Skip, int Take, string searchKey = "")
        {
            var driverData = await _driverRepository.GetDriverVehicleList(Skip, Take, searchKey);

            return new DriverDataTableDto()
            {
                TotalRecords = driverData.Total,
                FilterRecords = driverData.Filtered,
                DriverInfo = driverData.DriverVehicle.Select(d => new DriverVehicleDto
                {
                    DriverId = d.Driver.Id,
                    VehicleId = d.Vehicle.Id,
                    DriverName = d.Driver.GetFullName(),
                    DriverContact = d.Driver.PhoneNumber ?? string.Empty,
                    VehicleName = d.Vehicle.VehicleName ?? "Unknown",
                    SeatingCapacity = d.Vehicle.SeatingCapacity ?? 2,
                    VehicleThumbnail = d.VehicleMedia.ThumbnailUrl ?? "Unknown",
                    VehicleType = d.Vehicle.VehicleTypeId.ToString() ?? "Unknown",
                    Created = d.Driver.Created ?? DateTime.Now,
                    isApproved = d.Driver.IsApproved

                }).AsParallel()
            };

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

        public async Task<IEnumerable<DriverVehicleExportDto>> ExportAllAsync()
        {
            var driverVehicleExportData = await _driverRepository.ExportAllAsync();
            return driverVehicleExportData.Select(x => new DriverVehicleExportDto()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                LicenseNumber = x.LicenseNumber,
                AboutOn = x.AboutOn,
                AvailabilityStatus = x.AvailabilityStatus,
                ApproveDriver = x.ApproveDriver,
                VehicleName = x.VehicleName,
                AboutOnVehicle = x.AboutOnVehicle,
                VehicleNumber = x.VehicleNumber,
                SeatingCapacity = x.SeatingCapacity,
                Color = x.Color,
                Description = x.Description,
                Make = x.Make,
                Features = x.Features,
                Model = x.Model,
            });
        }

        public async Task<DriverVehicleInfoDto?> GetDriverVehicle(int DriverVehileId)
        {
            var dvInfo = await _driverRepository.GetDriverVehicle(DriverVehileId);
            if (dvInfo == null) return null;
            return new DriverVehicleInfoDto()
            {
                Driver = dvInfo.Driver == null ? null :
                        new DriverDto()
                        {
                            FirstName = dvInfo.Driver.FirstName,
                            LastName = dvInfo.Driver.LastName,
                            LicenseNumber = dvInfo.Driver.LicenseNumber,
                            Address = dvInfo.Driver.Address,
                            AboutOn = dvInfo.Driver.AboutOn,
                            Photo = dvInfo.Driver.Photo,
                            Email = dvInfo.Driver.Email,
                            AvailabilityStatus = dvInfo.Driver.AvailabilityStatus,
                            PhoneNumber = dvInfo.Driver.PhoneNumber,
                            VehicleType = dvInfo.Driver.VehicleType,
                            Id = dvInfo.Driver.Id,
                        },
                Vehicle = dvInfo.Vehicle == null ? null :
                        new VehicleDto()
                        {
                            VehicleName = dvInfo.Vehicle.VehicleName,
                            Features = dvInfo.Vehicle.Features,
                            Description = dvInfo.Vehicle.Description,
                            AboutOnVehicle = dvInfo.Vehicle.AboutOnVehicle,
                            Color = dvInfo.Vehicle.Color,
                            Make = dvInfo.Vehicle.Make,
                            Model = dvInfo.Vehicle.Model,
                            SeatingCapacity = dvInfo.Vehicle.SeatingCapacity,
                            VehicleNumber = dvInfo.Vehicle.VehicleNumber,
                            VehicleTypeId = dvInfo.Vehicle.VehicleTypeId,
                            Id = dvInfo.Vehicle.Id,
                        },
                VehicleMedia = dvInfo.VehicleMedia == null ? null :
                                new VehicleMediaDto()
                                {
                                    MediaName = dvInfo.VehicleMedia.MediaName,
                                    MediaType = dvInfo.VehicleMedia.MediaType,
                                    MediaUrl = dvInfo.VehicleMedia.MediaUrl,
                                    ThumbnailUrl = dvInfo.VehicleMedia.ThumbnailUrl,
                                    Id = dvInfo.VehicleMedia.Id,
                                }
            };
        }
    }
}

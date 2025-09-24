using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class DriverService(IDriverRepository driverRepository) : IDriverService
    {
        private readonly IDriverRepository _driverRepository = driverRepository;

        public async Task<int> ApproveDriverAsync(int DriverId, CancellationToken token)
        {
            return await _driverRepository.ApproveDriverAsync(DriverId, token);
        }

        public async Task<int> ApproveDriversAsync(List<int> DriverIds, CancellationToken token)
        {
            return await _driverRepository.ApproveDriversAsync(DriverIds, token);
        }

        public async Task<int> AssignVehicleAsync(int DriverId, int VehicleId, CancellationToken token)
        {
            return await _driverRepository.AssignVehicleAsync(DriverId, VehicleId, token);
        }

        public async Task<DriverDto?> GetDriverAsync(int DriverId, CancellationToken token)
        {
            var driverdata = await _driverRepository.GetDriverAsync(DriverId, token);
            if (driverdata == null) return null;
            return new DriverDto()
            {
                AboutOn = driverdata.AboutOn,
                Address = driverdata.Address,
                LastName = driverdata.LastName,
                FirstName = driverdata.FirstName,
                Email = driverdata.Email,
                LicenseNumber = driverdata.LicenseNumber,
                PhoneNumber = driverdata.PhoneNumber,
                Id = driverdata.Id,
                AvailabilityStatus = driverdata.AvailabilityStatus,
                Photo = driverdata.Photo,
                Created = driverdata.Created,
                IsVehicleAssigned = driverdata.IsVehicleAssigned,
                IsApproved = driverdata.IsApproved
            };
        }

        public async Task<DriverTableDto> GetDriverListAsync(string SearchValue, int Take, int Skip, CancellationToken token)
        {
            var DriverTable = await _driverRepository.GetDriverListAsync(SearchValue, Take, Skip, token);
            return new DriverTableDto()
            {
                TotalRecords = DriverTable.TotalRecords,
                FilterRecords = DriverTable.FilterRecords,
                Driverdtos = DriverTable.DriverEntities.Select(d => new DriverDto()
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    PhoneNumber = d.PhoneNumber,
                    Email = d.Email,
                    LastName = d.LastName,
                    AboutOn = d.AboutOn,
                    AvailabilityStatus = d.AvailabilityStatus,
                    Address = d.Address,
                    Created = d.Created.Value,
                    LicenseNumber = d.LicenseNumber,
                    Photo = d.Photo,
                    IsApproved = d.IsApproved ?? false,
                    IsVehicleAssigned = d.IsVehicleAssigned
                })
            };
        }

        public async Task<int> RejectDriverAsync(int DriverId, CancellationToken token)
        {
            return await _driverRepository.ApproveDriverAsync(DriverId, token);
        }

        public async Task<int> RejectDriversAsync(List<int> DriverIds, CancellationToken token)
        {
            return await _driverRepository.RejectDriversAsync(DriverIds, token);
        }
        public async Task<IEnumerable<DriverExportDto>> ExportAllAsync(CancellationToken token)
        {
            var driverVehicleExportData = await _driverRepository.ExportAllAsync(token);
            return driverVehicleExportData.Select(x => new DriverExportDto()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                LicenseNumber = x.LicenseNumber,
                AboutOn = x.AboutOn,
                AvailabilityStatus = x.AvailabilityStatus,
                Id = x.Id,
                IsApproved = x.IsApproved ?? false,
            });
        }

        public async Task<IEnumerable<UnAssignedDriversDto>> GetUnAssignedDriversList(CancellationToken token)
        {
            var unassigned = await _driverRepository.GetUnAssignedDriversList(token);
            return [.. unassigned.Select(x => new UnAssignedDriversDto()
            {
                FullName = x.FullName,
                Id = x.Id,
                License = x.License
            })];
        }
        public async Task<IEnumerable<DriversDropdownDto>> GetDriversDropdownList(CancellationToken token)
        {
            var drivers = await _driverRepository.GetDriversDropdownList(token);
            return [.. drivers.Select(x => new DriversDropdownDto()
            {
                FullName = x.FullName,
                Id = x.Id,
                License = x.License
            })];
        }
    }
}

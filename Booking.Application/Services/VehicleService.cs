using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        public async Task<int> ApproveVehicleAsync(int VehicleId, CancellationToken token)
        {
            return await _vehicleRepository.ApproveVehicleAsync(VehicleId, token);
        }
        public async Task<int> ApproveVehiclesAsync(List<int> VehicleIds, CancellationToken token)
        {
            return await _vehicleRepository.ApproveVehiclesAsync(VehicleIds, token);
        }
        public async Task<int> AssignDriverAsync(int DriverId, int VehicleId, CancellationToken token)
        {
            return await _vehicleRepository.AssignDriverAsync(DriverId, VehicleId, token);
        }
        public async Task<IEnumerable<UnAssignedVehiclesDto>> GetUnAssignedVehiclesList(CancellationToken token)
        {
            var unassigned = await _vehicleRepository.GetUnAssignedVehiclesList(token);
            return unassigned.Select(v => new UnAssignedVehiclesDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber
            }).ToList();
        }
        public async Task<VehicleDto?> GetVehicleAsync(int VehicleId, CancellationToken token)
        {
            var vehicle = await _vehicleRepository.GetVehicleAsync(VehicleId, token);
            if (vehicle is null) return null;
            return new VehicleDto()
            {
                Id = vehicle.Id,
                ModelName = vehicle.ModelName,
                VehicleNumber = vehicle.VehicleNumber,
                Color = vehicle.Color,
                Make = vehicle.Make,
                AboutOnVehicle = vehicle.AboutOnVehicle,
                DefaultImage = vehicle.DefaultImage,
                CreatedOn = vehicle.CreatedOn,
                BasePrice = vehicle.BasePrice,
                TaxRate = vehicle.TaxRate,
                FuelType = vehicle.FuelType,
                IsActive = vehicle.IsActive,
                CreatedBy = vehicle.CreatedBy,
                UpdatedBy = vehicle.UpdatedBy,
                UpdatedOn = vehicle.UpdatedOn,
                OtherInfromation = vehicle.OtherInfromation,
            };
        }
        public async Task<IEnumerable<VehicleDropdownDto>> GetVehicleDropdownList(CancellationToken token)
        {
            var vehicleList = await _vehicleRepository.GetVehicleDropdownList(token);
            return [.. vehicleList.Select(x => new VehicleDropdownDto()
            {
                ModelName = x.ModelName,
                VehicleId = x.VehicleId,
                RegistrationNumber = x.RegistrationNumber
            })];
        }
        public async Task<VehicleTableDto> GetVehicleListAsync(string SearchValue, int Take, int Skip, CancellationToken token)
        {
            var vehicles = await _vehicleRepository.GetVehicleListAsync(SearchValue, Take, Skip, token);
            return new VehicleTableDto()
            {
                TotalRecords = vehicles.TotalRecords,
                FilterRecords = vehicles.FilterRecords,
                VehicleDtos = vehicles.VehicleEntities.Select(d => new VehicleDto()
                {
                    Id = d.Id,
                    ModelName = d.ModelName,
                    VehicleNumber = d.VehicleNumber,
                    Color = d.Color,
                    Make = d.Make,
                    AboutOnVehicle = d.AboutOnVehicle,
                    DefaultImage = d.DefaultImage,
                    CreatedOn = d.CreatedOn,
                    BasePrice = d.BasePrice,
                    TaxRate = d.TaxRate,
                    FuelType = d.FuelType,
                    IsActive = d.IsActive,
                })
            };
        }
        public async Task<int> RejectVehicleAsync(int VehicleId, CancellationToken token)
        {
            return await _vehicleRepository.RejectVehicleAsync(VehicleId, token);
        }
        public async Task<int> RejectVehiclesAsync(List<int> VehicleIds, CancellationToken token)
        {
            return await _vehicleRepository.ApproveVehiclesAsync(VehicleIds, token);
        }
    }
}

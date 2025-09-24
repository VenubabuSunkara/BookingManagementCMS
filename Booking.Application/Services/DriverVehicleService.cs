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
    public class DriverVehicleService(IDriverVehicleRepository driverVehicleRepository) : IDriverVehicleService
    {
        private readonly IDriverVehicleRepository _driverVehicleRepository = driverVehicleRepository;
        public async Task<DriverVehicleTableDto> DriverVehicleList(string SearchValue, int Take, int Skip, CancellationToken token)
        {
            var driverVehicleList = await _driverVehicleRepository.DriverVehicleList(SearchValue, Take, Skip, token);
            return new DriverVehicleTableDto()
            {
                Total = driverVehicleList.Total,
                Filtered = driverVehicleList.Filtered,
                DriverVehicle = driverVehicleList.DriverVehicle.Select(x => new DriverVehicleDto()
                {
                    Driver = new DriverDto()
                    {
                        Id = x.Driver.Id,
                        AboutOn = x.Driver.AboutOn,
                        Address = x.Driver.Address,
                        FirstName = x.Driver.FirstName,
                        LastName = x.Driver.LastName,
                        Photo = x.Driver.Photo,
                        PhoneNumber = x.Driver.PhoneNumber,
                        LicenseNumber = x.Driver.LicenseNumber,
                        Email = x.Driver.Email,
                        IsApproved = x.Driver.IsApproved,
                    },
                    Vehicle = new VehicleDto()
                    {
                        DefaultImage = x.Vehicle.DefaultImage,
                        AboutOnVehicle = x.Vehicle.AboutOnVehicle,
                        BasePrice = x.Vehicle.BasePrice,
                        Color = x.Vehicle.Color,
                        FuelType = x.Vehicle.FuelType,
                        Id = x.Vehicle.Id,
                        Make = x.Vehicle.Make,
                        ModelName = x.Vehicle.ModelName,
                        VehicleNumber = x.Vehicle.VehicleNumber,
                        OtherInfromation = x.Vehicle.OtherInfromation,
                        TaxRate = x.Vehicle.TaxRate,
                    }
                })
            };
        }

        public async Task<int> RejectDriverVehicleAsync(int DriverId, int VehicleId, CancellationToken token)
        {
            return await _driverVehicleRepository.RejectDriverVehicleAsync(DriverId, VehicleId, token);
        }
    }
}

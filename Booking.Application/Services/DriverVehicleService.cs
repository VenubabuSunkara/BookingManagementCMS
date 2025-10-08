using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Amazon.S3.Util.S3EventNotification;

namespace Booking.Application.Services
{
    public class DriverVehicleService(IDriverVehicleRepository driverVehicleRepository) : IDriverVehicleService
    {
        private readonly IDriverVehicleRepository _driverVehicleRepository = driverVehicleRepository;

        public async Task<int> CreateDriverVehicle(CreateDriverVehicleDto entity, CancellationToken token)
        {
            return await _driverVehicleRepository.CreateDriverVehicle(new CreateDriverVehicleEntity()
            {
                DriverEntity = new CreateDriverEntity()
                {
                    PhoneNumber = entity.DriverDto.PhoneNumber,
                    FirstName = entity.DriverDto.FirstName,
                    LastName = entity.DriverDto.LastName,
                    Email = entity.DriverDto.Email,
                    AboutOn = entity.DriverDto.AboutOn,
                    Address = entity.DriverDto.Address,
                    DateOfBirth = entity.DriverDto.DateOfBirth,
                    UpdatedOn = entity.DriverDto.UpdatedOn,
                    UpdatedBy = entity.DriverDto.UpdatedBy,
                    CreatedBy = entity.DriverDto.CreatedBy,
                    CreatedOn = entity.DriverDto.CreatedOn,
                    UserName = entity.DriverDto.UserName,
                    Gender = entity.DriverDto.Gender,
                    AvailabilityStatus = entity.DriverDto.AvailabilityStatus,
                    ApproveDriver = entity.DriverDto.ApproveDriver,
                    IsActive = entity.DriverDto.IsActive,
                    Photo = entity.DriverDto.Photo,
                    TenantId = entity.DriverDto.TenantId,
                    Password = entity.DriverDto.Password,
                    LicenseNumber = entity.DriverDto.LicenseNumber,
                },
                VehicleEntity = new CreateVehicleEntity()
                {
                    VehicleNumber = entity.VehicleDto.VehicleNumber,
                    Model = entity.VehicleDto.Model,
                    Color = entity.VehicleDto.Color,
                    Fare = entity.VehicleDto.Fare,
                    CarName = entity.VehicleDto.CarName,
                    AverageMileage = entity.VehicleDto.AverageMileage,
                    Fecility = entity.VehicleDto.Fecility,
                    PollucationCertificationNumber = entity.VehicleDto.PollucationCertificationNumber,
                    InsurenceValidUntil = entity.VehicleDto.InsurenceValidUntil,
                    InsurnceNumber = entity.VehicleDto.InsurnceNumber,
                    VehicleTypeId = entity.VehicleDto.VehicleTypeId,
                    AboutOnVehicle = entity.VehicleDto.AboutOnVehicle,
                    DefaultImage = entity.VehicleDto.DefaultImage,
                    UpdatedOn = entity.VehicleDto.UpdatedOn,
                    UpdatedBy = entity.VehicleDto.UpdatedBy,
                    CreatedBy = entity.VehicleDto.CreatedBy,
                    CreatedOn = entity.VehicleDto.CreatedOn,
                },
                VehicleDefaultImagejson = entity.VehicleDefaultImagejson,
                DriverPhotojson = entity.DriverPhotojson
            }, token);
        }

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

        public async Task<CreateDriverVehicleDto?> GetDriverVehicleById(int DriverId, int VehicleId, CancellationToken token)
        {
            var driverVehicle = await _driverVehicleRepository.GetDriverVehicleById(DriverId, VehicleId, token);
            if (driverVehicle is null) return null;
            return new CreateDriverVehicleDto()
            {
                DriverDto = new CreateDriverDto()
                {
                    PhoneNumber = driverVehicle.DriverEntity.PhoneNumber,
                    FirstName = driverVehicle.DriverEntity.FirstName,
                    LastName = driverVehicle.DriverEntity.LastName,
                    Email = driverVehicle.DriverEntity.Email,
                    AboutOn = driverVehicle.DriverEntity.AboutOn,
                    Address = driverVehicle.DriverEntity.Address,
                    DateOfBirth = driverVehicle.DriverEntity.DateOfBirth,
                    UpdatedOn = driverVehicle.DriverEntity.UpdatedOn,
                    UpdatedBy = driverVehicle.DriverEntity.UpdatedBy,
                    CreatedBy = driverVehicle.DriverEntity.CreatedBy,
                    CreatedOn = driverVehicle.DriverEntity.CreatedOn,
                    Gender = driverVehicle.DriverEntity.Gender,
                    AvailabilityStatus = driverVehicle  .DriverEntity.AvailabilityStatus,
                    ApproveDriver = driverVehicle.DriverEntity.ApproveDriver,
                    IsActive = driverVehicle.DriverEntity.IsActive,
                    Photo = driverVehicle.DriverEntity.Photo,
                    TenantId = driverVehicle.DriverEntity.TenantId,
                    LicenseNumber = driverVehicle.DriverEntity.LicenseNumber,
                },
                VehicleDto= new CreateVehicleDto ()
                {
                    VehicleNumber = driverVehicle.VehicleEntity.VehicleNumber,
                    Model = driverVehicle.VehicleEntity.Model,
                    Color = driverVehicle.VehicleEntity.Color,
                    Fare = driverVehicle.VehicleEntity.Fare,
                    CarName = driverVehicle.VehicleEntity.CarName,
                    AverageMileage = driverVehicle.VehicleEntity.AverageMileage,
                    Fecility = driverVehicle.VehicleEntity.Fecility,
                    PollucationCertificationNumber = driverVehicle.VehicleEntity.PollucationCertificationNumber,
                    InsurenceValidUntil = driverVehicle.VehicleEntity.InsurenceValidUntil,
                    InsurnceNumber = driverVehicle.VehicleEntity.InsurnceNumber,
                    VehicleTypeId = driverVehicle.VehicleEntity.VehicleTypeId,
                    AboutOnVehicle = driverVehicle.VehicleEntity.AboutOnVehicle,
                    DefaultImage = driverVehicle.VehicleEntity.DefaultImage,
                    UpdatedOn = driverVehicle.VehicleEntity.UpdatedOn,
                    UpdatedBy = driverVehicle.VehicleEntity.UpdatedBy,
                    CreatedBy = driverVehicle.VehicleEntity.CreatedBy,
                    CreatedOn = driverVehicle.VehicleEntity.CreatedOn,
                    VehicleId = driverVehicle.VehicleEntity.VehicleId,
                },
            };
        }

        public async Task<int> RejectDriverVehicleAsync(int DriverId, int VehicleId, CancellationToken token)
        {
            return await _driverVehicleRepository.RejectDriverVehicleAsync(DriverId, VehicleId, token);
        }

        public async Task<int> UpdateDriverVehicle(CreateDriverVehicleDto entity, CancellationToken token)
        {
            return await _driverVehicleRepository.UpdateDriverVehicle(new CreateDriverVehicleEntity()
            {

            }, token);
        }
    }
}

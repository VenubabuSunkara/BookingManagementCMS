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
    public class BookingService(IBookingRepository bookingRepository) : IBookingService
    {
        private readonly IBookingRepository _bookingRepository = bookingRepository;

        public async Task<IEnumerable<BookingOrderDto>> GetAllBookings(int VehicleId)
        {
            var bookings = await _bookingRepository.GetAllBookings(VehicleId);
            return bookings.Select(x => new BookingOrderDto()
            {
                VehicleId = x.VehicleId,
                BookingDate = x.BookingDate,
                Status = x.Status,
                CouponCodeId = x.CouponCodeId,
                PackageId = x.PackageId,
                CustomerId = x.CustomerId,
                TotalAmount = x.TotalAmount,
                TravelDate = x.TravelDate,
                Id = x.Id,
                Vehicle = new VehicleDto()
                {
                    VehicleName = x.Vehicle.VehicleNumber,
                    Id = x.Vehicle.Id,
                    VehicleNumber = x.Vehicle.VehicleNumber,
                    Description = x.Vehicle.Description,
                    AboutOnVehicle = x.Vehicle.AboutOnVehicle,
                    Color = x.Vehicle.Color,
                    Make = x.Vehicle.Make,
                    Model = x.Vehicle.Model,
                    SeatingCapacity = x.Vehicle.SeatingCapacity,
                    Features = x.Vehicle.Features,
                    VehicleTypeId = x.Vehicle.VehicleTypeId
                },
                BookingDetails = [.. x.BookingDetails.Select(y => new BookingDetailsDto()
                {
                    Id = y.Id,
                    PassengerAge = y.PassengerAge,
                    PassengerGender = y.PassengerGender,
                    PassengerName = y.PassengerName,
                    BookingId = y.BookingId,
                    RelativeId = y.RelativeId,
                })]
            }).AsParallel();
        }

        public async Task<IEnumerable<BookingOrderDto>> GetAllBookings(int VehicleId, int Year)
        {
            var bookings = await _bookingRepository.GetAllBookings(VehicleId, Year);
            return bookings.Select(x => new BookingOrderDto()
            {
                VehicleId = x.VehicleId,
                BookingDate = x.BookingDate,
                Status = x.Status,
                CouponCodeId = x.CouponCodeId,
                PackageId = x.PackageId,
                CustomerId = x.CustomerId,
                TotalAmount = x.TotalAmount,
                TravelDate = x.TravelDate,
                Id = x.Id,
                BookingDetails = [.. x.BookingDetails.Select(y => new BookingDetailsDto()
                {
                    Id = y.Id,
                    PassengerAge = y.PassengerAge,
                    PassengerGender = y.PassengerGender,
                    PassengerName = y.PassengerName,
                    BookingId = y.BookingId,
                    RelativeId = y.RelativeId,
                })]
            }).AsParallel();
        }

        public async Task<BookingsDataTableDto> GetAllBookings(int Skip, int Take, string searchKey = "")
        {
            var bookings = await _bookingRepository.GetAllBookings(Skip, Take, searchKey);
            return new BookingsDataTableDto()
            {
                TotalRecords = bookings.Total,
                FilterRecords = bookings.Filtered,
                BookingsInfo = [.. bookings.BookingOrders.Select(x => new BookingOrderDto()
                {
                    VehicleId = x.VehicleId,
                    BookingDate = x.BookingDate,
                    Status = x.Status,
                    CouponCodeId = x.CouponCodeId,
                    PackageId = x.PackageId,
                    CustomerId = x.CustomerId,
                    TotalAmount = x.TotalAmount,
                    TravelDate = x.TravelDate,
                    Id = x.Id,
                    Vehicle = new VehicleDto()
                    {
                        VehicleName = x.Vehicle.VehicleNumber,
                        Id = x.Vehicle.Id,
                        VehicleNumber = x.Vehicle.VehicleNumber,
                        Description = x.Vehicle.Description,
                        AboutOnVehicle = x.Vehicle.AboutOnVehicle,
                        Color = x.Vehicle.Color,
                        Make = x.Vehicle.Make,
                        Model = x.Vehicle.Model,
                        SeatingCapacity = x.Vehicle.SeatingCapacity,
                        Features = x.Vehicle.Features,
                        VehicleTypeId = x.Vehicle.VehicleTypeId
                    },
                    Driver=new DriverDto (){
                        FullName=x.Driver.GetFullName(),
                        LicenseNumber  = x.Driver.LicenseNumber,
                        Email=x.Driver.Email,
                        PhoneNumber=x.Driver.PhoneNumber,
                        AboutOn=x.Driver.AboutOn,
                        Address=x.Driver.Address,
                        AvailabilityStatus=x.Driver.IsDriverAvailable(),
                    },
                    BookingDetails = [.. x.BookingDetails.Select(y => new BookingDetailsDto()
                        {
                            Id = y.Id,
                            PassengerAge = y.PassengerAge,
                            PassengerGender = y.PassengerGender,
                            PassengerName = y.PassengerName,
                            BookingId = y.BookingId,
                            RelativeId = y.RelativeId,
                        })]
                })]
            };
        }
    }
}

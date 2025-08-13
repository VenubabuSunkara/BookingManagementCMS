using Booking.Application.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class BookingRepository(BookingCmsContext context) : IBookingRepository
    {
        private readonly BookingCmsContext _context = context;
        public async Task<IEnumerable<BookingOrder>> GetAllBookings(int VehicleId)
        {
            var bookings = await _context.BookingOrders.Include(x => x.BookingDetails)
                                         .Where(x => x.VehicleId.Equals(VehicleId))
                                         .ToListAsync();
            return bookings.Select(x => new BookingOrder
            {
                BookingDate = x.BookingDate,
                TravelDate = x.TravelDate,
                CouponCodeId = x.CouponCodeId,
                CustomerId = x.CustomerId,
                Id = x.Id,
                PackageId = x.PackageId,
                Status = x.Status,
                TotalAmount = x.TotalAmount,
                VehicleId = x.VehicleId,
                BookingDetails = [.. x.BookingDetails.Select(y => new BookingDetailsEntity()
                {
                    BookingId = y.BookingId,
                    Id = y.Id,
                    PassengerName = y.PassengerName,
                    PassengerAge = y.PassengerAge,
                    PassengerGender = y.PassengerGender,
                    RelativeId = y.RelativeId,
                })]
            }).AsParallel();
        }
        public async Task<IEnumerable<BookingOrder>> GetAllBookings(int VehicleId, int Year)
        {
            var bookings = await _context.BookingOrders.Include(x => x.BookingDetails)
                                         .Where(x => x.VehicleId.Equals(VehicleId) && x.CreatedOn.Value.Year == Year)
                                         .ToListAsync();
            return bookings.Select(x => new BookingOrder
            {
                BookingDate = x.BookingDate,
                TravelDate = x.TravelDate,
                CouponCodeId = x.CouponCodeId,
                CustomerId = x.CustomerId,
                Id = x.Id,
                PackageId = x.PackageId,
                Status = x.Status,
                TotalAmount = x.TotalAmount,
                VehicleId = x.VehicleId,
                BookingDetails = [.. x.BookingDetails.Select(y => new BookingDetailsEntity()
                {
                    BookingId = y.BookingId,
                    Id = y.Id,
                    PassengerName = y.PassengerName,
                    PassengerAge = y.PassengerAge,
                    PassengerGender = y.PassengerGender,
                    RelativeId = y.RelativeId,
                })]
            }).AsParallel();
        }
        public async Task<BookingsDTable> GetAllBookings(int Skip, int Take, string searchKey = "")
        {
            var totalCount = await _context.BookingOrders.AsNoTracking().CountAsync();
            var bookings = await _context.BookingOrders
                                        .AsNoTracking()
                                        .AsSplitQuery()
                                        .Include(x => x.BookingDetails)
                                        .Include(x => x.Package)
                                        .Include(x => x.Customer)
                                        .Select(mapping => new
                                        {
                                            Id = mapping.Id,
                                            mapping.BookingDate,
                                            mapping.TravelDate,
                                            mapping.TotalAmount,
                                            mapping.VehicleId,
                                            mapping.CustomerId,
                                            mapping.CouponCodeId,
                                            mapping.PackageId,
                                            mapping.Status,
                                            Driver = new
                                            {
                                                mapping.Driver.Id,
                                                mapping.Driver.FirstName,
                                                mapping.Driver.LastName,
                                                mapping.Driver.Email,
                                                mapping.Driver.PhoneNumber,
                                                mapping.Driver.Photo,
                                                mapping.Driver.Address,
                                                mapping.Driver.LicenseNumber,
                                                mapping.Driver.AboutOn,
                                                mapping.Driver.AvailabilityStatus,
                                                mapping.Driver.ApproveDriver,
                                                mapping.Driver.CreatedAt
                                            },
                                            Vehicle = new
                                            {
                                                mapping.Vehicle.Id,
                                                mapping.Vehicle.VehicleName,
                                                mapping.Vehicle.Color,
                                                mapping.Vehicle.Description,
                                                mapping.Vehicle.AboutOnVehicle,
                                                mapping.Vehicle.Features,
                                                mapping.Vehicle.Make,
                                                mapping.Vehicle.VehicleNumber,
                                                mapping.Vehicle.SeatingCapacity,
                                                mapping.Vehicle.Model,
                                                mapping.Vehicle.VehicleTypeId,
                                                DefaultMedia = mapping.Vehicle.VehicleMedia
                                                                    .Where(m => m.IsDefault)
                                                                    .Select(m => new
                                                                    {
                                                                        m.MediaName,
                                                                        m.MediaType,
                                                                        m.MediaUrl,
                                                                        m.ThumbnailUrl
                                                                    }).FirstOrDefault()
                                            },
                                            BookingDetails = mapping.BookingDetails.Select(y => new
                                            {
                                                y.BookingId,
                                                y.Id,
                                                y.PassengerName,
                                                y.PassengerAge,
                                                y.PassengerGender,
                                                y.RelativeId
                                            }).ToList()
                                        })
                                        .Skip(Skip)
                                        .Take(Take)
                                        .ToListAsync();
            return new BookingsDTable
            {
                Total = totalCount,
                BookingOrders = [..bookings.Select(x => new BookingOrder()
                {
                    BookingDate = x.BookingDate,
                    TravelDate = x.TravelDate,
                    CouponCodeId = x.CouponCodeId,
                    CustomerId = x.CustomerId,
                    Id = x.Id,
                    PackageId = x.PackageId,
                    Status = x.Status,
                    TotalAmount = x.TotalAmount,
                    VehicleId = x.VehicleId,
                    Vehicle=new Vehicle(){
                        VehicleName=x.Vehicle.VehicleName,
                        VehicleNumber=x.Vehicle.VehicleNumber,
                        Color=x.Vehicle.Color,
                        Features=x.Vehicle.Features,
                        AboutOnVehicle=x.Vehicle.AboutOnVehicle,
                        Make=x.Vehicle.Make,
                        Model=x.Vehicle.Model,
                        SeatingCapacity=x.Vehicle.SeatingCapacity,
                        Description=x.Vehicle.Description,
                    },
                    Driver=new Driver (){
                        AboutOn =x.Driver.AboutOn,
                        Address=x.Driver.Address,
                        AvailabilityStatus=x.Driver.AvailabilityStatus,
                        Email=x.Driver.Email,
                        FirstName=x.Driver.FirstName,
                        LastName=x.Driver.LastName,
                        IsApproved=x.Driver.ApproveDriver,
                        LicenseNumber=x.Driver.LicenseNumber,
                        PhoneNumber=x.Driver.PhoneNumber,
                    },
                    BookingDetails = [.. x.BookingDetails.Select(y => new BookingDetailsEntity()
                    {
                        BookingId = y.BookingId,
                        Id = y.Id,
                        PassengerName = y.PassengerName,
                        PassengerAge = y.PassengerAge,
                        PassengerGender = y.PassengerGender,
                        RelativeId = y.RelativeId
                    })]
                })],
                Filtered = totalCount
            };
        }
    }
}

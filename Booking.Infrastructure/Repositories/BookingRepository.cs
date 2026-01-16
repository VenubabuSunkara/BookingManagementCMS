using Booking.Application.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Booking.Infrastructure.Repositories
{
    public class BookingRepository(BookingCmsContext context) : IBookingRepository
    {
        private readonly BookingCmsContext _context = context;

        public async Task<BookingOrderTableEntity> GetCustomerBookings(string CustomerId, int Skip, int Take, CancellationToken token)
        {
            var q = _context.BookingOrders.AsNoTracking();
            var total = await q.CountAsync(token);
            if (!string.IsNullOrEmpty(CustomerId))
                q = q.Where(x => x.CustomerId.Equals(CustomerId));
            q = q.OrderByDescending(d => d.CreatedOn);
            var filtered = await q.CountAsync(token);
            var page = await q.Skip(Skip).Take(Take).ToListAsync(token);
            return new BookingOrderTableEntity
            {
                TotalRecords = total,
                FilterRecords = filtered,
                BookingOrderEntities = [.. page.Select(d => new BookingOrderEntity
                {
                  BookingOrderId=d.BookingOrderId,
                  BookingDate=d.BookingDate,
                  EstimatedFare=d.EstimatedFare,
                  CustomerId=d.CustomerId,
                  VehicleId=d.VehicleId,
                  DriverId=d.DriverId,
                  PickupLocation=d.PickupLocation,
                  DropLocation=d.DropLocation,
                  ScheduledDropTime=d.ScheduledDropTime,
                  ActualFare=d.ActualFare,
                  BookingNumber=d.BookingNumber,
                  PaymentStatus=d.PaymentStatus,
                  ScheduledPickupTime=d.ScheduledPickupTime,
                  TripType=d.TripType,
                  Status=d.Status,
                  CreatedAt=d.CreatedOn,
                })]
            };
        }

        public async Task<BookingOrderTableEntity> GetVehicleBookings(int VehicleId, int DriverId,
            int Skip, int Take, CancellationToken token)
        {
            var q = _context.BookingOrders.AsNoTracking();
            var total = await q.CountAsync(token);
            if (VehicleId != 0)
                q = q.Where(x => x.VehicleId.Equals(VehicleId));
            if (DriverId != 0)
                q = q.Where(x => x.DriverId.Equals(DriverId));
            q = q.OrderByDescending(d => d.CreatedOn);
            var filtered = await q.CountAsync(token);
            var page = await q.Skip(Skip).Take(Take).ToListAsync(token);
            return new BookingOrderTableEntity
            {
                TotalRecords = total,
                FilterRecords = filtered,
                BookingOrderEntities = [.. page.Select(d => new BookingOrderEntity
                {
                  BookingOrderId=d.BookingOrderId,
                  BookingDate=d.BookingDate,
                  EstimatedFare=d.EstimatedFare,
                  CustomerId=d.CustomerId,
                  VehicleId=d.VehicleId,
                  DriverId=d.DriverId,
                  PickupLocation=d.PickupLocation,
                  DropLocation=d.DropLocation,
                  ScheduledDropTime=d.ScheduledDropTime,
                  ActualFare=d.ActualFare,
                  BookingNumber=d.BookingNumber,
                  PaymentStatus=d.PaymentStatus,
                  ScheduledPickupTime=d.ScheduledPickupTime,
                  TripType=d.TripType,
                  Status=d.Status,
                  CreatedAt=d.CreatedOn,
                })]
            };
        }
        public async Task<BookingOrderTableEntity> GetDriverBookings(int DriverId, int Skip, int Take, CancellationToken token)
        {
            var q = _context.BookingOrders.AsNoTracking();
            var total = await q.CountAsync(token);
            if (DriverId != 0)
                q = q.Where(x => x.DriverId.Equals(DriverId));
            q = q.OrderByDescending(d => d.CreatedOn);
            var filtered = await q.CountAsync(token);
            var page = await q.Skip(Skip).Take(Take).ToListAsync(token);
            return new BookingOrderTableEntity
            {
                TotalRecords = total,
                FilterRecords = filtered,
                BookingOrderEntities = [.. page.Select(d => new BookingOrderEntity
                {
                  BookingOrderId=d.BookingOrderId,
                  BookingDate=d.BookingDate,
                  EstimatedFare=d.EstimatedFare,
                  CustomerId=d.CustomerId,
                  VehicleId=d.VehicleId,
                  DriverId=d.DriverId,
                  PickupLocation=d.PickupLocation,
                  DropLocation=d.DropLocation,
                  ScheduledDropTime=d.ScheduledDropTime,
                  ActualFare=d.ActualFare,
                  BookingNumber=d.BookingNumber,
                  PaymentStatus=d.PaymentStatus,
                  ScheduledPickupTime=d.ScheduledPickupTime,
                  TripType=d.TripType,
                  Status=d.Status,
                  CreatedAt=d.CreatedOn,
                })]
            };
        }

        public async Task<BookingOrderTableEntity> GetAllBookings(int Skip, int Take, CancellationToken token, string searchKey = "")
        {
            var q = _context.BookingOrders.AsNoTracking();
            var total = await q.CountAsync(token);
            if (!string.IsNullOrEmpty(searchKey))
                q = q.Where(x => x.BookingNumber.Equals(searchKey));
            q = q.OrderByDescending(d => d.CreatedOn);
            var filtered = await q.CountAsync(token);
            var page = await q.Skip(Skip).Take(Take).ToListAsync(token);
            return new BookingOrderTableEntity
            {
                TotalRecords = total,
                FilterRecords = filtered,
                BookingOrderEntities = [.. page.Select(d => new BookingOrderEntity
                {
                  BookingOrderId=d.BookingOrderId,
                  BookingDate=d.BookingDate,
                  EstimatedFare=d.EstimatedFare,
                  CustomerId=d.CustomerId,
                  VehicleId=d.VehicleId,
                  DriverId=d.DriverId,
                  PickupLocation=d.PickupLocation,
                  DropLocation=d.DropLocation,
                  ScheduledDropTime=d.ScheduledDropTime,
                  ActualFare=d.ActualFare,
                  BookingNumber=d.BookingNumber,
                  PaymentStatus=d.PaymentStatus,
                  ScheduledPickupTime=d.ScheduledPickupTime,
                  TripType=d.TripType,
                  Status=d.Status,
                  CreatedAt=d.CreatedOn,
                })]
            };
        }
    }
}

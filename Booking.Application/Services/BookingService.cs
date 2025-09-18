using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class BookingService(IBookingRepository bookingRepository) : IBookingService
    {
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        public async Task<BookingOrderTableDto> GetAllBookings(int Skip, int Take, CancellationToken token, string searchKey = "")
        {
            var BookingOrders = await _bookingRepository.GetAllBookings(Skip, Take, token, searchKey);
            return new BookingOrderTableDto()
            {
                TotalRecords = BookingOrders.TotalRecords,
                FilterRecords = BookingOrders.FilterRecords,
                BookingOrders = BookingOrders.BookingOrderEntities.
                Select(x => new BookingOrderDto()
                {
                    BookingOrderId = x.BookingOrderId,
                    DriverId = x.DriverId,
                    DropLocation = x.DropLocation,
                    BookingDate = x.BookingDate,
                    ScheduledDropTime = x.ScheduledDropTime,
                    ActualFare = x.ActualFare,
                    BookingNumber = x.BookingNumber,
                    CustomerId = x.CustomerId,
                    EstimatedFare = x.EstimatedFare,
                    PaymentStatus = x.PaymentStatus,
                    PickupLocation = x.PickupLocation,
                    ScheduledPickupTime = x.ScheduledPickupTime,
                    TripType = x.TripType,
                    VehicleId = x.VehicleId,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status,
                })
            };
        }
        public async Task<BookingOrderTableDto> GetCustomerBookings(string CustomerId, int Skip, int Take, CancellationToken token)
        {
            var BookingOrders = await _bookingRepository.GetCustomerBookings(CustomerId, Skip, Take, token);
            return new BookingOrderTableDto()
            {
                TotalRecords = BookingOrders.TotalRecords,
                FilterRecords = BookingOrders.FilterRecords,
                BookingOrders = BookingOrders.BookingOrderEntities.
                Select(x => new BookingOrderDto()
                {
                    BookingOrderId = x.BookingOrderId,
                    DriverId = x.DriverId,
                    DropLocation = x.DropLocation,
                    BookingDate = x.BookingDate,
                    ScheduledDropTime = x.ScheduledDropTime,
                    ActualFare = x.ActualFare,
                    BookingNumber = x.BookingNumber,
                    CustomerId = x.CustomerId,
                    EstimatedFare = x.EstimatedFare,
                    PaymentStatus = x.PaymentStatus,
                    PickupLocation = x.PickupLocation,
                    ScheduledPickupTime = x.ScheduledPickupTime,
                    TripType = x.TripType,
                    VehicleId = x.VehicleId,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status,
                })
            };
        }
        public async Task<BookingOrderTableDto> GetDriverBookings(int DriverId, int Skip, int Take, CancellationToken token)
        {
            var BookingOrders = await _bookingRepository.GetDriverBookings(DriverId, Skip, Take, token);
            return new BookingOrderTableDto()
            {
                TotalRecords = BookingOrders.TotalRecords,
                FilterRecords = BookingOrders.FilterRecords,
                BookingOrders = BookingOrders.BookingOrderEntities.
                Select(x => new BookingOrderDto()
                {
                    BookingOrderId = x.BookingOrderId,
                    DriverId = x.DriverId,
                    DropLocation = x.DropLocation,
                    BookingDate = x.BookingDate,
                    ScheduledDropTime = x.ScheduledDropTime,
                    ActualFare = x.ActualFare,
                    BookingNumber = x.BookingNumber,
                    CustomerId = x.CustomerId,
                    EstimatedFare = x.EstimatedFare,
                    PaymentStatus = x.PaymentStatus,
                    PickupLocation = x.PickupLocation,
                    ScheduledPickupTime = x.ScheduledPickupTime,
                    TripType = x.TripType,
                    VehicleId = x.VehicleId,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status,
                })
            };
        }
        public async Task<BookingOrderTableDto> GetVehicleBookings(int VehicleId, int DriverId, int Skip, int Take, CancellationToken token)
        {
            var BookingOrders = await _bookingRepository.GetVehicleBookings(VehicleId, DriverId, Skip, Take, token);
            return new BookingOrderTableDto()
            {
                TotalRecords = BookingOrders.TotalRecords,
                FilterRecords = BookingOrders.FilterRecords,
                BookingOrders = BookingOrders.BookingOrderEntities.
                Select(x => new BookingOrderDto()
                {
                    BookingOrderId = x.BookingOrderId,
                    DriverId = x.DriverId,
                    DropLocation = x.DropLocation,
                    BookingDate = x.BookingDate,
                    ScheduledDropTime = x.ScheduledDropTime,
                    ActualFare = x.ActualFare,
                    BookingNumber = x.BookingNumber,
                    CustomerId = x.CustomerId,
                    EstimatedFare = x.EstimatedFare,
                    PaymentStatus = x.PaymentStatus,
                    PickupLocation = x.PickupLocation,
                    ScheduledPickupTime = x.ScheduledPickupTime,
                    TripType = x.TripType,
                    VehicleId = x.VehicleId,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status,
                })
            };
        }
    }
}

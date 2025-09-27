using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class PackageLocationRepository(BookingCmsContext context) : IPackageLocationRepository
    {
        private readonly BookingCmsContext _context = context;
        public async Task<int> SavePackageLocation(TourLocationEntity LocationEntity, CancellationToken token)
        {
            await _context.TourLocations.AddAsync(new Data.Models.TourLocation()
            {
                PackageId = LocationEntity.PackageId,
                LocationName = LocationEntity.LocationName ?? string.Empty,
                Latitude = LocationEntity.Latitude,
                Longitude = LocationEntity.Longitude,
                CreatedBy = LocationEntity.CreatedBy,
                CreatedOn = LocationEntity.CreatedOn,
                UpdatedBy = LocationEntity.UpdatedBy,
                UpdatedOn = LocationEntity.UpdatedOn,
                Description = LocationEntity.Description,
                State = LocationEntity.State,
                Address = LocationEntity.Address,
                City = LocationEntity.City,
                Country = LocationEntity.Country,
                ZipCode = LocationEntity.ZipCode,
                IsActive = true,
                RouteDistance = LocationEntity.RouteDistance,
                RouteDuration = LocationEntity.RouteDuration,
                PointImage = LocationEntity.PointImage,
                LocationHeadLine = LocationEntity.LocationHeadLine,
                ViaLocations = LocationEntity.ViaLocations,
                FullAddress = $"{LocationEntity.Address}, {LocationEntity.City}, {LocationEntity.State}, {LocationEntity.Country} - {LocationEntity.ZipCode}",
            }, token);
            return await _context.SaveChangesAsync(token);
        }
    }
}

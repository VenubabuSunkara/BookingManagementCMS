using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class PackageLocationService(IPackageLocationRepository locationRepository) : IPackageLocationService
    {
        public IPackageLocationRepository _locationRepository = locationRepository;
        public async Task<int> SavePackageLocation(TourLocationDto Locationdto, CancellationToken token)
        {
            return await _locationRepository.SavePackageLocation(new TourLocationEntity()
            {
                PackageId = Locationdto.PackageId,
                LocationName = Locationdto.LocationName ?? string.Empty,
                Latitude = Locationdto.Latitude,
                Longitude = Locationdto.Longitude,
                CreatedBy = Locationdto.CreatedBy,
                CreatedOn = Locationdto.CreatedOn ?? DateTime.UtcNow,
                UpdatedBy = Locationdto.UpdatedBy,
                UpdatedOn = Locationdto.UpdatedOn,
                Description = Locationdto.Description,
                State = Locationdto.State,
                Address = Locationdto.Address,
                City = Locationdto.City,
                Country = Locationdto.Country,
                ZipCode = Locationdto.ZipCode,
                RouteDistance = Locationdto.RouteDistance,
                RouteDuration = Locationdto.RouteDuration,
                PointImage = Locationdto.PointImage,
                LocationHeadLine = Locationdto.LocationHeadLine,
                ViaLocations = Locationdto.ViaLocations,
            }, token);
        }
    }
}

using Booking.Application.DTOs.Tour;

namespace Booking.Application.Interfaces
{
    public interface IPackageLocationService
    {
        Task<int> SavePackageLocation(TourLocationDto LocationEntity, CancellationToken token);
    }
}

using Booking.Domain.Entities.Tour;

namespace Booking.Domain.Interfaces
{
    public interface IPackageLocationRepository
    {
        Task<int> SavePackageLocation(TourLocationEntity LocationEntity, CancellationToken token);
    }
}

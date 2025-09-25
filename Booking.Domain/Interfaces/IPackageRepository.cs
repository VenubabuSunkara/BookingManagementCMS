using Booking.Domain.Entities.Tour;

namespace Booking.Domain.Interfaces
{
    public interface IPackageRepository
    {
        Task<TourPackageTable> GetPackages(int Skip, int Take, string searchKey = "", int CategoryId = 0);
        Task<int> SavePackage(TourPackageEntity tourPackage, CancellationToken token);
    }
}

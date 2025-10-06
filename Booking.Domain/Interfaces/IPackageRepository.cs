using Booking.Domain.Entities.Tour;

namespace Booking.Domain.Interfaces
{
    public interface IPackageRepository
    {
        Task<TourPackageTable> GetPackages(int Skip, int Take, string searchKey, int CategoryId, CancellationToken token);
        Task<int> SavePackage(TourPackageEntity tourPackage, CancellationToken token);
        Task<TourPackageEntity?> GetPackage(int PackageId, CancellationToken token);
        Task<int> DeletePackage(int PackageId, CancellationToken token);
        Task<int> UpdatePackage(TourPackageEntity tourPackage, CancellationToken token);
        Task<IEnumerable<PackageDropdownEntity>> GetTrourPackageDrodown(CancellationToken token);
        Task UpdateMediaAsync(int packageId, List<PackageMediaEntity> media, CancellationToken token);
        Task UpdateLocationsAsync(int packageId, TourLocationEntity location, CancellationToken token);
    }
}

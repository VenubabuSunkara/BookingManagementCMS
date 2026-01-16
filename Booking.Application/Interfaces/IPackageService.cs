using Booking.Application.DTOs.Tour;

namespace Booking.Application.Interfaces
{
    public interface IPackageService
    {
        Task<PackageDataTableDto> GetPackages(int Skip, int Take, string searchKey, int CategoryId, CancellationToken token);
        Task<int> SavePackage(TourPackageDto tourPackage, CancellationToken token);
        Task<TourPackageDto?> GetPackage(int PackageId, CancellationToken token);
        Task<int> DeletePackage(int PackageId, CancellationToken token);
        Task<int> UpdatePackage(TourPackageDto tourPackage, CancellationToken token);
        Task<IEnumerable<PackageDropdownDto>> GetTrourPackageDrodown(CancellationToken token);
    }
}

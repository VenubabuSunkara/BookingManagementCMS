using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class PackageService(IPackageRepository packageRepository) : IPackageService
    {
        private readonly IPackageRepository _packageRepository = packageRepository;
        public async Task<PackageDataTableDto> GetPackages(int Skip, int Take, string searchKey = "", int CategoryId = 0)
        {

            var TourPackageList = await _packageRepository.GetPackages(Skip, Take, searchKey, CategoryId);
            return new PackageDataTableDto()
            {
                TotalRecords = TourPackageList.Total,
                FilterRecords = TourPackageList.Filtered,
                PackagesData = TourPackageList.PackageEntities.Select(x => new TourPackageDto()
                {
                    Price = x.BasePrice,
                    DurationDays = x.DurationDays,
                    FullDescription = x.FullDescription,
                    ShortDescription = x.ShortDescription,
                    Id = x.ItemId,
                    PackageName = x.PackageName,
                    Destination = x.Location.LocationName,
                    BannerImage = x.BannerImage,
                })
            };
        }

        public async Task<int> SavePackage(TourPackageDto tourPackage, CancellationToken token)
        {
            return await _packageRepository.SavePackage(new Domain.Entities.Tour.TourPackageEntity()
            {
                PackageName = tourPackage.PackageName,
                DurationDays = tourPackage.DurationDays,
                BasePrice = tourPackage.Price,
                BannerImage = tourPackage.BannerImage,
                FullDescription = tourPackage.FullDescription,
                ShortDescription = tourPackage.ShortDescription,
                CreatedBy=tourPackage.CreatedBy,
                UpdatedBy=tourPackage.UpdatedBy,
                CreatedOn= tourPackage.CreatedOn,
                UpdatedOn= tourPackage.UpdatedOn,
                ItemGuid= tourPackage.ItemGuid,
            }, token);
        }
    }
}

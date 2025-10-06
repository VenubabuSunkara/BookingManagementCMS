using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class PackageService(IPackageRepository packageRepository) : IPackageService
    {
        private readonly IPackageRepository _packageRepository = packageRepository;

        public async Task<int> DeletePackage(int PackageId, CancellationToken token)
        {
            return await _packageRepository.DeletePackage(PackageId, token);
        }

        public async Task<TourPackageDto?> GetPackage(int PackageId, CancellationToken token)
        {
            var package = await _packageRepository.GetPackage(PackageId, token);
            if (package == null) return null;
            return new TourPackageDto()
            {
                Price = package.BasePrice,
                DurationDays = package.DurationDays,
                FullDescription = package.FullDescription,
                ShortDescription = package.ShortDescription,
                Id = package.ItemId,
                PackageName = package.PackageName,
                BannerImage = package.BannerImage,
                CategoryId = package.CategoryId,
                CreatedBy = package.CreatedBy,
                CreatedOn = package.CreatedOn,
                Inclusions = package.Inclusions,
                ItemGuid = package.ItemGuid,
                ThingsToNote = package.ThingsToNote,
                UpdatedBy = package.UpdatedBy,
                UpdatedOn = package.UpdatedOn,
                Location = new TourLocationDto()
                {
                    LocationId = package.Location.LocationId,
                    LocationName = package.Location.LocationName ?? string.Empty,
                    RouteDistance = package.Location.RouteDistance,
                    State = package.Location.State,
                    Address = package.Location.Address,
                    City = package.Location.City,
                    RouteDuration = package.Location.RouteDuration,
                    Country = package.Location.Country,
                    Latitude = package.Location.Latitude,
                    LocationHeadLine = package.Location.LocationHeadLine,
                    Longitude = package.Location.Longitude,
                    PointImage = package.Location.PointImage,
                    ViaLocations = package.Location.ViaLocations,
                    ZipCode = package.Location.ZipCode,
                    Description = package.Location.Description,
                    CreatedBy = package.Location.CreatedBy,
                    CreatedOn = package.Location.CreatedOn,
                    UpdatedBy = package.Location.UpdatedBy,
                    UpdatedOn = package.Location.UpdatedOn,
                },
                PackageMedia = [.. package.PackageMedia.Select(x => new PackageMediaDto()
                {
                    Id = x.Id,
                    MediaUrl = x.MediaUrl,
                    MediaType = x.MediaType,
                    FileName = x.Filename,
                    CreatedBy = x.CreatedBy,
                    ThumbnailImage = x.ThumbnailImage,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                })]
            };
        }

        public async Task<PackageDataTableDto> GetPackages(int Skip, int Take, string searchKey, int CategoryId, CancellationToken token)
        {

            var TourPackageList = await _packageRepository.GetPackages(Skip, Take, searchKey, CategoryId, token);
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
                    BannerImage = x.BannerImage,
                    Inclusions = x.Inclusions,
                    ThingsToNote = x.ThingsToNote,
                    Location = new TourLocationDto()
                    {
                        LocationId = x.Location.LocationId,
                        LocationName = x.Location.LocationName ?? string.Empty,
                        RouteDistance = x.Location.RouteDistance,
                        State = x.Location.State,
                        Address = x.Location.Address,
                        City = x.Location.City,
                        RouteDuration = x.Location.RouteDuration,
                        Country = x.Location.Country,
                        Latitude = x.Location.Latitude,
                        LocationHeadLine = x.Location.LocationHeadLine,
                        Longitude = x.Location.Longitude,
                        PointImage = x.Location.PointImage,
                        ViaLocations = x.Location.ViaLocations,
                        ZipCode = x.Location.ZipCode,
                        Description = x.Location.Description
                    },
                })
            };
        }

        public async Task<IEnumerable<PackageDropdownDto>> GetTrourPackageDrodown(CancellationToken token)
        {
            var packageDropdowndata = await _packageRepository.GetTrourPackageDrodown(token);
            return packageDropdowndata.Select(x => new PackageDropdownDto()
            {
                PackageId = x.PackageId,
                PackageName = x.PackageName,
            });
        }

        public async Task<int> SavePackage(TourPackageDto tourPackage, CancellationToken token)
        {
            return await _packageRepository.SavePackage(new TourPackageEntity()
            {
                PackageName = tourPackage.PackageName,
                DurationDays = tourPackage.DurationDays,
                BasePrice = tourPackage.Price,
                BannerImage = tourPackage.BannerImage,
                FullDescription = tourPackage.FullDescription,
                ShortDescription = tourPackage.ShortDescription,
                CategoryId = tourPackage.CategoryId,
                CreatedBy = tourPackage.CreatedBy,
                UpdatedBy = tourPackage.UpdatedBy,
                Inclusions = tourPackage.Inclusions,
                ThingsToNote = tourPackage.ThingsToNote,
                CreatedOn = tourPackage.CreatedOn,
                UpdatedOn = tourPackage.UpdatedOn,
                ItemGuid = tourPackage.ItemGuid,
            }, token);
        }

        public async Task<int> UpdatePackage(TourPackageDto tourPackage, CancellationToken token)
        {
            return await _packageRepository.UpdatePackage(new TourPackageEntity()
            {
                ItemId = tourPackage.Id,
                PackageName = tourPackage.PackageName,
                DurationDays = tourPackage.DurationDays,
                BasePrice = tourPackage.Price,
                BannerImage = tourPackage.BannerImage,
                FullDescription = tourPackage.FullDescription,
                ShortDescription = tourPackage.ShortDescription,
                CategoryId = tourPackage.CategoryId,
                CreatedBy = tourPackage.CreatedBy,
                UpdatedBy = tourPackage.UpdatedBy,
                Inclusions = tourPackage.Inclusions,
                ThingsToNote = tourPackage.ThingsToNote,
                CreatedOn = tourPackage.CreatedOn,
                UpdatedOn = tourPackage.UpdatedOn,
                ItemGuid = tourPackage.ItemGuid,
                Location = new TourLocationEntity()
                {
                    LocationId = tourPackage.Location.LocationId,
                    LocationName = tourPackage.Location.LocationName,
                    RouteDistance = tourPackage.Location.RouteDistance,
                    State = tourPackage.Location.State,
                    Address = tourPackage.Location.Address,
                    City = tourPackage.Location.City,
                    RouteDuration = tourPackage.Location.RouteDuration,
                    Country = tourPackage.Location.Country,
                    Latitude = tourPackage.Location.Latitude,
                    LocationHeadLine = tourPackage.Location.LocationHeadLine,
                    Longitude = tourPackage.Location.Longitude,
                    PointImage = tourPackage.Location.PointImage,
                    ViaLocations = tourPackage.Location.ViaLocations,
                    ZipCode = tourPackage.Location.ZipCode,
                    Description = tourPackage.Location.Description,
                    CreatedBy = tourPackage.Location.CreatedBy,
                    CreatedOn = tourPackage.Location.CreatedOn,
                    UpdatedBy = tourPackage.Location.UpdatedBy,
                    UpdatedOn = tourPackage.Location.UpdatedOn,
                },
                PackageMedia = [.. tourPackage.PackageMedia.Select(x => new PackageMediaEntity()
                {
                    Id = x.Id,
                    MediaUrl = x.MediaUrl,
                    MediaType = x.MediaType,
                    Filename = x.FileName,
                    CreatedBy = x.CreatedBy,
                    ThumbnailImage = x.ThumbnailImage,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                })]
            }, token);
        }
    }
}

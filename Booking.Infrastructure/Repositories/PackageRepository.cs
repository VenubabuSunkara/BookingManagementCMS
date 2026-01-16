using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Booking.Infrastructure.Repositories
{
    public class PackageRepository(BookingCmsContext context, ILogger<PackageRepository> logger) : IPackageRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly ILogger<PackageRepository> _logger = logger;
        public async Task<int> DeletePackage(int PackageId, CancellationToken token)
        {
            await _context.TourPackages.Where(x => x.ItemId.Equals(PackageId)).ExecuteDeleteAsync(token);
            await _context.TourPackageMedia.Where(x => x.PackageId.Equals(PackageId)).ExecuteDeleteAsync(token);
            await _context.TourLocations.Where(x => x.PackageId.Equals(PackageId)).ExecuteDeleteAsync(token);
            return 1;
        }

        public Task<TourPackageEntity?> GetPackage(int PackageId, CancellationToken token)
        {
            IQueryable<TourPackage> query = _context.TourPackages.AsNoTracking();
            return query.Where(x => x.ItemId.Equals(PackageId)).Select(x => new TourPackageEntity()
            {
                PackageName = x.PackageName,
                DurationDays = x.DurationDays,
                BasePrice = x.BasePrice,
                BannerImage = x.BannerImage,
                FullDescription = x.Description,
                ShortDescription = x.ShortDescription,
                Inclusions = x.Inclusions,
                ThingsToNote = x.ThingsToNote,
                CategoryId = x.CategoryId,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy,
                CreatedOn = x.CreatedOn,
                UpdatedOn = x.UpdatedOn,
                ItemGuid = x.ItemGuid,
                ItemId = x.ItemId,
                Location = x.TourLocations.Any() &&
                    x.TourLocations != null ?
                    new TourLocationEntity()
                    {
                        LocationName = x.TourLocations.FirstOrDefault().LocationName,
                        LocationId = x.TourLocations.FirstOrDefault().LocationId,
                    } : new TourLocationEntity(),
                PackageMedia = x.TourPackageMedia.Any() &&
                    x.TourPackageMedia != null ?
                    x.TourPackageMedia.Select(m => new PackageMediaEntity()
                    {
                        MediaUrl = m.MediaUrl,
                        MediaType = m.MediaType,
                        PackageId = m.PackageId,
                        Id = m.MediaId
                    }).ToList() : new List<PackageMediaEntity>(),

            }).FirstOrDefaultAsync(token)!;

        }

        public Task<IEnumerable<PackageDropdownEntity>> GetTrourPackageDrodown(CancellationToken token)
        {
            IQueryable<TourPackage> query = _context.TourPackages.AsNoTracking();
            return Task.FromResult(query.Select(x => new PackageDropdownEntity()
            {
                PackageId = x.ItemId,
                PackageName = x.PackageName
            }).AsEnumerable());
        }
        public async Task<TourPackageTable> GetPackages(int Skip, int Take, string searchKey, int CategoryId, CancellationToken token)
        {
            IQueryable<TourPackage> query = _context.TourPackages.AsNoTracking().Include(x => x.TourLocations).AsNoTracking();
            var total = await query.CountAsync(token);
            if (CategoryId != 0)
            {
                query = query.Where(x => x.CategoryId == CategoryId);
            }
            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(x => x.PackageName.Contains(searchKey));
            }
            int filterdCount = await query.CountAsync(token);
            var TourPackageList = await query.Select(x => new TourPackageEntity()
            {
                PackageName = x.PackageName,
                DurationDays = x.DurationDays.ToString(),
                BasePrice = x.BasePrice,
                BannerImage = x.BannerImage,
                FullDescription = x.Description,
                ShortDescription = x.ShortDescription,
                Inclusions = x.Inclusions,
                ThingsToNote = x.ThingsToNote,
                CategoryId = x.CategoryId,
                ItemId = x.ItemId,
                Location = x.TourLocations.Any() &&
                    x.TourLocations != null ?
                    new TourLocationEntity()
                    {
                        LocationName = x.TourLocations.FirstOrDefault().LocationName,
                        LocationId = x.TourLocations.FirstOrDefault().LocationId,
                    } : new TourLocationEntity()
            }).ToListAsync(token);
            return new TourPackageTable()
            {
                Total = total,
                Filtered = filterdCount,
                PackageEntities = TourPackageList
            };
        }

        public async Task<int> SavePackage(TourPackageEntity tourPackage, CancellationToken token)
        {
            var entity = new TourPackage
            {
                PackageName = tourPackage.PackageName,
                DurationDays = tourPackage.DurationDays,
                BasePrice = tourPackage.BasePrice,
                BannerImage = tourPackage.BannerImage,
                Description = tourPackage.FullDescription,
                ShortDescription = tourPackage.ShortDescription,
                Inclusions = tourPackage.Inclusions,
                ThingsToNote = tourPackage.ThingsToNote,
                CategoryId = tourPackage.CategoryId,
                CreatedBy = tourPackage.CreatedBy,
                UpdatedBy = tourPackage.UpdatedBy,
                CreatedOn = tourPackage.CreatedOn,
                UpdatedOn = tourPackage.UpdatedOn,
                ItemGuid = tourPackage.ItemGuid,
            };
            await _context.TourPackages.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
            return entity.ItemId;
        }
        public async Task UpdateLocationsAsync(int packageId, TourLocationEntity location, CancellationToken token)
        {
            //// Remove locations not in the new list
            //await _context.TourLocations
            //    .Where(l => l.PackageId == packageId &&
            //           !locations.Select(dto => dto.LocationId).Contains(l.LocationId))
            //    .ExecuteDeleteAsync();

            //// Update existing locations
            //foreach (var location in locations.Where(l => l.LocationId != 0))
            //{
            await _context.TourLocations
                .Where(l => l.PackageId == packageId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.LocationName, location.LocationName)
                    .SetProperty(x => x.Address, location.Address)
                    .SetProperty(x => x.City, location.City)
                    .SetProperty(x => x.State, location.State)
                    .SetProperty(x => x.Country, location.Country)
                    .SetProperty(x => x.ZipCode, location.ZipCode)
                    .SetProperty(x => x.Latitude, location.Latitude)
                    .SetProperty(x => x.Longitude, location.Longitude)
                    .SetProperty(x => x.UpdatedBy, location.UpdatedBy)
                    .SetProperty(x => x.UpdatedOn, location.UpdatedOn)
                    .SetProperty(x => x.ViaLocations, location.ViaLocations)
                    .SetProperty(x => x.LocationHeadLine, location.LocationHeadLine)
                    .SetProperty(x => x.PointImage, location.PointImage)
                    .SetProperty(x => x.RouteDistance, location.RouteDistance)
                    .SetProperty(x => x.RouteDuration, location.RouteDuration)
                    .SetProperty(x => x.Description, location.Description), token);
            //}

            //// Insert new locations
            //var newLocations = locations
            //    .Where(l => l.LocationId == 0)
            //    .Select(l => new TourLocation
            //    {
            //        TourPackageId = packageId,
            //        Name = l.Name,
            //        Description = l.Description
            //    })
            //    .ToList();

            //if (newLocations.Any())
            //{
            //    await _context.TourLocations.AddRangeAsync(newLocations);
            //    await _context.SaveChangesAsync();
            //}
        }
        public async Task UpdateMediaAsync(int packageId, List<PackageMediaEntity> media, CancellationToken token)
        {
            // Remove media not in the new list
            await _context.TourPackageMedia
                .Where(m => m.PackageId == packageId &&
                       !media.Select(dto => dto.Id).Contains(m.MediaId))
                .ExecuteDeleteAsync(cancellationToken: token);

            // Update existing media
            foreach (var mediaItem in media.Where(m => m.Id != 0))
            {
                await _context.TourPackageMedia
                    .Where(m => m.MediaId == mediaItem.Id && m.PackageId == packageId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(x => x.Caption, mediaItem.Filename)
                        .SetProperty(x => x.ThumbnailUrl, mediaItem.ThumbnailImage)
                        .SetProperty(x => x.MediaUrl, mediaItem.MediaUrl)
                        .SetProperty(x => x.MediaType, mediaItem.MediaType)
                        .SetProperty(x => x.UpdatedBy, mediaItem.UpdatedBy)
                        .SetProperty(x => x.UpdatedOn, mediaItem.UpdatedAt), token);
            }

            // Insert new media
            var newMedia = media
                .Where(m => m.Id == 0)
                .Select(m => new TourPackageMedium
                {
                    PackageId = packageId,
                    MediaType = m.MediaType,
                    Caption = m.Filename,
                    MediaUrl = m.MediaUrl,
                    ThumbnailUrl = m.ThumbnailImage,
                    CreatedBy = m.CreatedBy,
                    CreatedOn = m.CreatedAt,
                    UpdatedBy = m.UpdatedBy,
                    UpdatedOn = m.UpdatedAt,
                }).ToList();

            if (newMedia.Count != 0)
            {
                await _context.TourPackageMedia.AddRangeAsync(newMedia, token);
                await _context.SaveChangesAsync(token);
            }
        }
        public async Task<int> UpdatePackage(TourPackageEntity tourPackage, CancellationToken token)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(token);
            try
            {
                // Update main package details using ExecuteUpdateAsync
                var updateResult = await _context.TourPackages
                    .Where(tp => tp.ItemId == tourPackage.ItemId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(x => x.PackageName, tourPackage.PackageName)
                        .SetProperty(x => x.Description, tourPackage.FullDescription)
                        .SetProperty(x => x.BasePrice, tourPackage.BasePrice)
                        .SetProperty(x => x.DurationDays, tourPackage.DurationDays)
                        .SetProperty(x => x.CategoryId, tourPackage.CategoryId)
                        .SetProperty(x => x.BannerImage, tourPackage.BannerImage)
                        .SetProperty(x => x.ShortDescription, tourPackage.ShortDescription)
                        .SetProperty(x => x.ThingsToNote, tourPackage.ThingsToNote)
                        .SetProperty(x => x.Inclusions, tourPackage.Inclusions)
                        .SetProperty(x => x.UpdatedOn, tourPackage.UpdatedOn)
                        .SetProperty(x => x.UpdatedBy, tourPackage.UpdatedBy), token);

                if (updateResult == 0)
                    return 0;

                // Update Locations
                await UpdateLocationsAsync(tourPackage.ItemId, tourPackage.Location, token);

                // Update Media
                await UpdateMediaAsync(tourPackage.ItemId, tourPackage.PackageMedia, token);

                await transaction.CommitAsync(token);
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tour package {ItemId}", tourPackage.ItemId);
                await transaction.RollbackAsync(token);
                throw;
            }
        }


    }
}

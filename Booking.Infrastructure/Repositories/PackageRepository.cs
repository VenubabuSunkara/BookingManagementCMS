using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class PackageRepository(BookingCmsContext context) : IPackageRepository
    {
        private readonly BookingCmsContext _context = context;

        public async Task<TourPackageTable> GetPackages(int Skip, int Take, string searchKey, int CategoryId)
        {
            IQueryable<TourPackage> query = _context.TourPackages
                .Include(x => x.TourDestinations)
                .ThenInclude(x => x.Location);
            var total = await query.CountAsync();
            if (CategoryId > 0)
            {
                query = query.Where(x => x.CategoryId == CategoryId);
            }
            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(x => x.PackageName.Contains(searchKey));
            }
            int filterdCount = await query.CountAsync();
            var TourPackageList = await query.Select(x => new TourPackageEntity()
            {
                PackageName = x.PackageName,
                DurationDays = x.DurationDays.ToString(),
                BasePrice = x.BasePrice,
                BannerImage = x.BannerImage,
                FullDescription = x.Description,
                ShortDescription = x.ShortDescription,
                Location = x.TourDestinations.Any() && x.TourDestinations.FirstOrDefault()!.Location != null ? new LocationEntity()
                {
                    Name = x.TourDestinations.FirstOrDefault()!.Location!.Name,
                    LocationId = x.TourDestinations.FirstOrDefault()!.Location!.LocationId,
                } : new LocationEntity(),
            }).ToListAsync();
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
                DurationDays = int.Parse(tourPackage.DurationDays),
                BasePrice = tourPackage.BasePrice,
                BannerImage = tourPackage.BannerImage,
                Description = tourPackage.FullDescription,
                ShortDescription = tourPackage.ShortDescription,
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
    }
}

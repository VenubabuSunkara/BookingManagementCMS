using Booking.Application.DTOs;
using Booking.Domain.Entities;
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

        public async Task<TourPackageTable> GetPackages(int Skip, int Take, string searchKey = "", int CategoryId = 0)
        {
            IQueryable<TourPackage> query = _context.TourPackages
                .Include(x => x.TourDestinations)
                .ThenInclude(x => x.Location);
            if (CategoryId > 0)
            {
                query = query.Where(x => x.CategoryId == CategoryId);
            }
            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(x => x.PackageName.Contains(searchKey));
            }
            int total = query.Count();
            var TourPackageList = await query.Select(x => new TourPackageEntity()
            {
                PackageName = x.PackageName,
                DurationDays = x.DurationDays,
                BasePrice = x.BasePrice,
                BannerImage = x.BannerImage,
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                Location = x.TourDestinations.Select(
                    y => new LocationEntity()
                    {
                        City = y.Location.City,
                        Country = y.Location.Country,
                        Name = y.Location.Name,
                        State = y.Location.State,
                        LocationId = y.Location.LocationId
                    }).FirstOrDefault() ?? new LocationEntity()
            }).ToListAsync();
            return new TourPackageTable()
            {
                Total = total,
                Filtered = total,
                PackageEntities = TourPackageList
            };
        }
    }
}

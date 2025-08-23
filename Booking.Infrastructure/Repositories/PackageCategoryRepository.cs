using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class PackageCategoryRepository(BookingCmsContext context) : IPackageCategoryRepository
    {
        private readonly BookingCmsContext _context = context;

        public async Task<IEnumerable<TourPackageCategoryEntity>> GetTourPackageCategory()
        {
            return await _context.TourPackageCategories.Include(x => x.TourPackages)
                .Select(x => new TourPackageCategoryEntity()
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    NoOfPackages = x.TourPackages.Count()
                }).ToListAsync();
        }
    }
}

using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using static Amazon.S3.Util.S3EventNotification;

namespace Booking.Infrastructure.Repositories
{
    public class PackageCategoryRepository(BookingCmsContext context) : IPackageCategoryRepository
    {
        private readonly BookingCmsContext _context = context;

        public async Task<int> CreateCategoryAsync(TourPackageCategoryEntity entity, CancellationToken token)
        {
            var category = new TourPackageCategory
            {
                CategoryName = entity.CategoryName,
                Description = entity.Description
            };
            _context.TourPackageCategories.Add(category);
            await _context.SaveChangesAsync(token);
            return category.Id; // return new Category I
        }

        public async Task<int> DeleteCategoryAsync(int CategoryId, CancellationToken token)
        {
            var category = await _context.TourPackageCategories.FirstOrDefaultAsync(x => x.Id == CategoryId, token);

            if (category == null)
                return 0; // nothing to delete

            _context.TourPackageCategories.Remove(category);
            return await _context.SaveChangesAsync(token);
        }

        public async Task<TourPackageCategoryEntity> GetCategoryAsync(int CategoryId, CancellationToken token)
        {
            var category = await _context.TourPackageCategories
                              .AsNoTracking()
                              .Where(x => x.Id == CategoryId)
                              .Select(x => new TourPackageCategoryEntity
                              {
                                  Id = x.Id,
                                  CategoryName = x.CategoryName,
                                  Description = x.Description,
                                  NoOfPackages = x.TourPackages.Count()
                              }).FirstOrDefaultAsync(token);
            return category!;
        }

        public async Task<IEnumerable<TourPackageCategoryEntity>> GetTourPackageCategory(CancellationToken token)
        {
            return await _context.TourPackageCategories.AsNoTracking()
                         .Select(x => new TourPackageCategoryEntity
                         {
                             Id = x.Id,
                             CategoryName = x.CategoryName,
                             Description = x.Description,
                             NoOfPackages = x.TourPackages.Count()
                         }).ToListAsync(token);
        }
        public async Task<int> UpdateCategoryAsync(TourPackageCategoryEntity entity, CancellationToken token)
        {
            var category = new TourPackageCategory
            {
                Id = entity.Id,
                CategoryName = entity.CategoryName,
                Description = entity.Description
            };

            _context.TourPackageCategories.Attach(category);
            _context.Entry(category).Property(x => x.CategoryName).IsModified = true;
            _context.Entry(category).Property(x => x.Description).IsModified = true;

            return await _context.SaveChangesAsync(token);
        }
        public async Task<IEnumerable<TourPackageCategoryEntity>> ExportAllAsync(CancellationToken token)
        {
            return await _context.TourPackageCategories.AsNoTracking()
                .Select(x => new TourPackageCategoryEntity()
                {
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    Id = x.Id,
                    NoOfPackages = x.TourPackages.Count()
                }).ToListAsync(token);
        }
        public async Task ImportPackageCategoriesAsync(IEnumerable<TourPackageCategoryEntity> entities, CancellationToken token)
        {
            var bulkConfig = new BulkConfig { SetOutputIdentity = true, BatchSize = 4000 };
            await context.BulkInsertOrUpdateAsync(entities.Select(x => new TourPackageCategory()
            {
                CategoryName = x.CategoryName,
                Description = x.Description,
                Id = x.Id,
            }), b => b.SetOutputIdentity = true, cancellationToken: token); //BulkConfig with Action arg.
        }
    }
}

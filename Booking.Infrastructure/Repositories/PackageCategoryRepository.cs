using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class PackageCategoryRepository(BookingCmsContext context) : IPackageCategoryRepository
    {
        private readonly BookingCmsContext _context = context;

        public async Task<int> CreateCategoryAsync(TourPackageCategoryEntity entity, CancellationToken token)
        {
            if (await _context.TourPackageCategories.AsNoTracking().AnyAsync(x => x.CategoryName.Equals(entity.CategoryName), cancellationToken: token))
                return 0;
            var category = new TourPackageCategory
            {
                CategoryName = entity.CategoryName,
                Description = entity.Description,
                IsActive = entity.IsActive,
                CreatedBy = entity.CreatedBy,
                UpdatedOn = entity.UpdatedOn,
                CreatedOn = entity.CreatedOn,
                UpdatedBy = entity.UpdatedBy
            };
            _context.TourPackageCategories.Add(category);
            await _context.SaveChangesAsync(token);
            return category.Id;
        }

        public async Task<int> DeleteCategoryAsync(int CategoryId, CancellationToken token)
        {
            return await _context.TourPackageCategories.AsNoTracking()
                               .Where(x => x.Id.Equals(CategoryId))
                               .ExecuteDeleteAsync(cancellationToken: token);
        }

        public async Task<TourPackageCategoryEntity?> GetCategoryAsync(int CategoryId, CancellationToken token)
        {
            return await _context.TourPackageCategories
                               .AsNoTracking()
                               .Where(x => x.Id == CategoryId)
                               .Select(x => new TourPackageCategoryEntity
                               {
                                   Id = x.Id,
                                   CategoryName = x.CategoryName,
                                   Description = x.Description,
                                   IsActive = x.IsActive ?? false,
                                   NoOfPackages = x.TourPackages.Count
                               }).FirstOrDefaultAsync(token);
        }

        public async Task<IEnumerable<TourPackageCategoryEntity>> GetTourPackageCategory(CancellationToken token)
        {
            return await _context.TourPackageCategories.AsNoTracking()
                 .Select(x => new TourPackageCategoryEntity
                 {
                     Id = x.Id,
                     CategoryName = x.CategoryName,
                     Description = x.Description,
                     IsActive = x.IsActive ?? false,
                     NoOfPackages = x.TourPackages.Count
                 }).ToListAsync(token);
        }

        public async Task<int> UpdateCategoryAsync(TourPackageCategoryEntity entity, CancellationToken token)
        {
            return await _context.TourPackageCategories
                               .Where(x => x.Id.Equals(entity.Id))
                               .ExecuteUpdateAsync(c => c
                                   .SetProperty(s => s.CategoryName, entity.CategoryName)
                                   .SetProperty(s => s.IsActive, entity.IsActive)
                                   .SetProperty(s => s.UpdatedOn, entity.UpdatedOn)
                                   .SetProperty(s => s.UpdatedBy, entity.UpdatedBy)
                                   .SetProperty(s => s.Description, entity.Description),
                                    cancellationToken: token);
        }
        public async Task<IEnumerable<TourPackageCategoryEntity>> ExportAllAsync(CancellationToken token)
        {
            return await _context.TourPackageCategories.AsNoTracking()
                .Select(x => new TourPackageCategoryEntity()
                {
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    Id = x.Id,
                    IsActive = x.IsActive ?? false,
                    NoOfPackages = x.TourPackages.Count
                }).ToListAsync(token);
        }
        public async Task ImportPackageCategoriesAsync(IEnumerable<TourPackageCategoryEntity> entities, CancellationToken token)
        {
            var bulkConfig = new BulkConfig { SetOutputIdentity = true, BatchSize = 4000 };
            await _context.BulkInsertOrUpdateAsync(entities.Select(x => new TourPackageCategory()
            {
                CategoryName = x.CategoryName,
                Description = x.Description,
                Id = x.Id,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy,
                UpdatedOn = x.UpdatedOn,
                CreatedOn = x.CreatedOn,
                UpdatedBy = x.UpdatedBy
            }), bulkConfig, cancellationToken: token); //BulkConfig with Action arg.
        }

        public async Task<bool> IsExistsName(string categoryName, CancellationToken token)
        {
            return await _context.TourPackageCategories.AsNoTracking().AnyAsync(x => x.CategoryName.Equals(categoryName), cancellationToken: token);
        }
    }
}

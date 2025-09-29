using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class PackageCategoryService(IPackageCategoryRepository packageRepository) : IPackageCategoryService
    {
        private readonly IPackageCategoryRepository _packageRepository = packageRepository;

        public async Task<int> CreateCategoryAsync(TourPackageCategoryDto entity, CancellationToken token)
        {
            return await _packageRepository.CreateCategoryAsync(new TourPackageCategoryEntity()
            {
                CategoryName = entity.CategoryName,
                Description = entity.Description,
                IsActive = entity.IsActive ?? false,
                CreatedBy = entity.CreatedBy ?? string.Empty,
                CreatedOn = entity.CreatedOn ?? DateTime.UtcNow,
                UpdatedBy = entity.UpdatedBy ?? string.Empty,
                UpdatedOn = entity.UpdatedOn ?? DateTime.UtcNow

            }, token);
        }

        public async Task<int> DeleteCategoryAsync(int CategoryId, CancellationToken token)
        {
            return await _packageRepository.DeleteCategoryAsync(CategoryId, token);
        }

        public async Task<IEnumerable<TourPackageCategoryDto>> ExportAllAsync(CancellationToken token)
        {
            var packagecategoryList = await _packageRepository.ExportAllAsync(token);
            return packagecategoryList.Select(x => new TourPackageCategoryDto()
            {
                NoOfPackages = x.NoOfPackages,
                CategoryName = x.CategoryName,
                Description = x.Description,
                Id = x.Id,
                IsActive = x.IsActive
            });
        }

        public async Task<TourPackageCategoryDto> GetCategoryAsync(int CategoryId, CancellationToken token)
        {
            var category = await _packageRepository.GetCategoryAsync(CategoryId, token);
            return new TourPackageCategoryDto()
            {
                CategoryName = category.CategoryName,
                Description = category.Description,
                Id = category.Id,
                IsActive = category.IsActive,
                NoOfPackages = category.NoOfPackages,
            };
        }

        public async Task<IEnumerable<TourPackageCategoryDto>> GetTourPackageCategory(CancellationToken token)
        {
            var packagecategoryList = await _packageRepository.GetTourPackageCategory(token);
            return packagecategoryList.Select(x => new TourPackageCategoryDto()
            {
                NoOfPackages = x.NoOfPackages,
                CategoryName = x.CategoryName,
                Description = x.Description,
                IsActive = x.IsActive,
                Id = x.Id
            });
        }

        public async Task ImportPackageCategoriesAsync(IEnumerable<TourPackageCategoryDto> entities, CancellationToken token)
        {
            await _packageRepository.ImportPackageCategoriesAsync([.. entities.Select(x => new TourPackageCategoryEntity()
            {
                CategoryName = x.CategoryName,
                Description = x.Description,
                IsActive = x.IsActive ?? false,
                CreatedBy = x.CreatedBy ?? string.Empty,
                CreatedOn = x.CreatedOn ?? DateTime.UtcNow,
                UpdatedBy = x.UpdatedBy ?? string.Empty,
                UpdatedOn = x.UpdatedOn ?? DateTime.UtcNow
            })], token);
        }

        public async Task<int> UpdateCategoryAsync(TourPackageCategoryDto entity, CancellationToken token)
        {
            return await _packageRepository.UpdateCategoryAsync(new TourPackageCategoryEntity()
            {
                CategoryName = entity.CategoryName,
                Description = entity.Description,
                IsActive = entity.IsActive ?? false,
                Id = entity.Id ?? 0,
                CreatedBy = entity.CreatedBy ?? string.Empty,
                CreatedOn = entity.CreatedOn ?? DateTime.UtcNow,
                UpdatedBy = entity.UpdatedBy ?? string.Empty,
                UpdatedOn = entity.UpdatedOn ?? DateTime.UtcNow
            }, token);
        }
    }
}

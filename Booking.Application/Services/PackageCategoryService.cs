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
                NoOfPackages = entity.NoOfPackages,
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
                Id = x.Id
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
                Id = x.Id
            });
        }

        public async Task ImportPackageCategoriesAsync(IEnumerable<TourPackageCategoryDto> entities, CancellationToken token)
        {
            await _packageRepository.ImportPackageCategoriesAsync([.. entities.Select(x => new TourPackageCategoryEntity()
            {
                CategoryName = x.CategoryName,
                Description = x.Description,
            })], token);
        }

        public async Task<int> UpdateCategoryAsync(TourPackageCategoryDto entity, CancellationToken token)
        {
            return await _packageRepository.UpdateCategoryAsync(new TourPackageCategoryEntity()
            {
                CategoryName = entity.CategoryName,
                Description = entity.Description,
                Id = entity.Id,
                NoOfPackages = entity.NoOfPackages,
            }, token);
        }
    }
}

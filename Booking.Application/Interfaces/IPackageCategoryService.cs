using Booking.Application.DTOs.Tour;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface IPackageCategoryService
    {
        Task<IEnumerable<TourPackageCategoryDto>> GetTourPackageCategory(CancellationToken token);
        Task<int> CreateCategoryAsync(TourPackageCategoryDto entity, CancellationToken token);
        Task<TourPackageCategoryDto> GetCategoryAsync(int CategoryId, CancellationToken token);
        Task<int> UpdateCategoryAsync(TourPackageCategoryDto entity, CancellationToken token);
        Task<int> DeleteCategoryAsync(int CategoryId, CancellationToken token);
        Task<IEnumerable<TourPackageCategoryDto>> ExportAllAsync(CancellationToken token);
        Task ImportPackageCategoriesAsync(IEnumerable<TourPackageCategoryDto> entities, CancellationToken token);

    }
}

using Booking.Domain.Entities.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IPackageCategoryRepository
    {
        Task<IEnumerable<TourPackageCategoryEntity>> GetTourPackageCategory(CancellationToken token);
        Task<int> CreateCategoryAsync(TourPackageCategoryEntity entity, CancellationToken token);
        Task<TourPackageCategoryEntity> GetCategoryAsync(int CategoryId, CancellationToken token);
        Task<int> UpdateCategoryAsync(TourPackageCategoryEntity entity, CancellationToken token);
        Task<int> DeleteCategoryAsync(int CategoryId, CancellationToken token);
        Task<IEnumerable<TourPackageCategoryEntity>> ExportAllAsync(CancellationToken token);
        Task ImportPackageCategoriesAsync(IEnumerable<TourPackageCategoryEntity> entities, CancellationToken token);
    }
}

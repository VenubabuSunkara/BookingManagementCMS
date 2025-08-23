using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class PackageCategoryService(IPackageCategoryRepository packageRepository) : IPackageCategoryService
    {
        private readonly IPackageCategoryRepository _packageRepository = packageRepository;

        public async Task<IEnumerable<TourPackageCategoryDto>> GetTourPackageCategory()
        {
            var packagecategoryList = await _packageRepository.GetTourPackageCategory();
            return packagecategoryList.Select(x => new TourPackageCategoryDto()
            {
                NoOfPackages = x.NoOfPackages,
                CategoryName = x.CategoryName,
                Description = x.Description,
                Id = x.Id
            });
        }
    }
}

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
    public class PackageService(IPackageRepository packageRepository) : IPackageService
    {
        private readonly IPackageRepository _packageRepository = packageRepository;

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

        //public async Task<PackageDataTableDto> SearchPackages(int Skip, int Take, string searchKey = "")
        //{
        //    var packages = await _packageRepository.SearchPackage(Skip, Take, searchKey);
        //    return new PackageDataTableDto()
        //    {
        //        TotalRecords = packages.Total,
        //        FilterRecords = packages.Filtered,
        //        PackagesData = [.. packages.PackageEntities.Select(x => new PackageDto()
        //        {
        //            Price = x.Price,
        //            Destination = x.Destination,
        //            FullDescription = x.FullDescription,
        //            ShortDescription = x.ShortDescription,
        //            DurationDays = x.DurationDays,
        //            Source = x.Source,
        //            TurmsandConditions = x.TurmsandConditions,
        //            Title = x.Title,
        //            CreatedBy = x.CreatedBy,
        //            PackageMedia=x.PackageMedia==null?null:
        //                new PackageMediaDto(){
        //                    MediaType=x.PackageMedia.MediaType,
        //                    ThumbnailImage=x.PackageMedia.ThumbnailImage,
        //                    IsDefault=x.PackageMedia.IsDefault,
        //                    MediaUrl=x.PackageMedia.MediaUrl
        //                }
        //        })]
        //    };
        //}
    }
}

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
        public async Task<PackageDataTableDto> GetPackages(int Skip, int Take, string searchKey = "", int CategoryId = 0)
        {

            var TourPackageList = await _packageRepository.GetPackages(Skip, Take, searchKey, CategoryId);
            return new PackageDataTableDto()
            {
                TotalRecords = TourPackageList.Total,
                FilterRecords = TourPackageList.Filtered,
                PackagesData = TourPackageList.PackageEntities.Select(x => new TourPackageDto()
                {
                    Price = x.BasePrice,
                    DurationDays = x.DurationDays,
                    FullDescription = x.Description,
                    ShortDescription = x.ShortDescription,
                    Id = x.ItemId,
                    Title = x.PackageName,
                    Destination = x.Location.Destination,
                    BannerImage=x.BannerImage,
                    
                })
            };
        }
    }
}

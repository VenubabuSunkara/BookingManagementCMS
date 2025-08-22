using Booking.Application.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class PackageRepository(BookingCmsContext context) : IPackageRepository
    {
        private readonly BookingCmsContext _context = context;

        public Task<IEnumerable<TourPackageEntity>> GetPackages(int Skip, int Take, string searchKey = "", int CategoryId = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TourPackageCategoryEntity>> GetTourPackageCategory()
        {
            return await _context.TourPackageCategories.Include(x => x.TourPackages).Select(x => new TourPackageCategoryEntity()
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                Description = x.Description,
                NoOfPackages = x.TourPackages.Count()
            }).ToListAsync();
        }

        //public async Task<IEnumerable<TourPackageEntity>> GetPackages(int Skip, int Take, string searchKey = "", int CategoryId = 0)
        //{
        //    var totalCount = await _context.TourPackages.AsNoTracking().CountAsync();
        //    //var packages = await _context.TourPackages
        //    //    .Include(x=>x.TourDestinations)
        //    //    .Include(x=>x.TourItineraries)
        //    //    .Include(x=>x.TourReviews)
        //    //    .Include(x=>x.TourMediaGalleries)
        //    //                            .AsNoTracking()
        //    //                            .Select(mapping => new
        //    //                            {
        //    //                                ItemId = mapping.ItemId,
        //    //                                mapping.DurationDays,
        //    //                                mapping.Price,
        //    //                                mapping.PackageName,
        //    //                                mapping.Description,
        //    //                                mapping.CategoryId    
        //    //                            }).Where(x => x.IsActive == true)
        //    //                            .Skip(Skip)
        //    //                            .Take(Take)
        //    //                            .ToListAsync();

        //    //return new PackageDTable
        //    //{
        //    //    Total = totalCount,
        //    //    PackageEntities = [..packages.Select(x => new PackageEntity()
        //    //    {
        //    //        Id= x.Id,
        //    //        Price = x.Price,
        //    //        ShortDescription = x.ShortDescription,
        //    //        Source = x.Source,
        //    //        Destination = x.Destination,
        //    //        FullDescription = x.FullDescription,
        //    //        Title = x.Title,
        //    //        TurmsandConditions=x.TurmsandConditions,
        //    //        DurationDays = x.DurationDays,
        //    //        PackageMedia =x.PackageMedia ==null?null:
        //    //        new  PackageMediaEntity(){
        //    //            MediaType=x.PackageMedia.MediaType,
        //    //            MediaUrl=x.PackageMedia.MediaUrl,
        //    //            ThumbnailImage=x.PackageMedia.ThumbnailImage
        //    //        }
        //    //    })],
        //    //    Filtered = totalCount
        //    //};
        //}
    }
}

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

        //public async Task<PackageDTable> SearchPackage(int Skip, int Take, string searchKey = "")
        //{
        //    var totalCount = await _context.Packages.AsNoTracking().CountAsync();
        //    var packages = await _context.Packages
        //                                .AsNoTracking()
        //                                .Select(mapping => new
        //                                {
        //                                    Id = mapping.Id,
        //                                    mapping.Destination,
        //                                    mapping.DurationDays,
        //                                    mapping.Source,
        //                                    mapping.ShortDescription,
        //                                    mapping.FullDescription,
        //                                    mapping.Title,
        //                                    mapping.Price,
        //                                    mapping.IsActive,
        //                                    mapping.TurmsandConditions,
        //                                    PackageMedia = mapping.PackageMedia == null ? null :
        //                                        mapping.PackageMedia
        //                                        .Where(x => x.IsDefault == true)
        //                                        .Select(x => new
        //                                        {
        //                                            x.PackageId,
        //                                            x.Id,
        //                                            x.MediaType,
        //                                            x.MediaUrl,
        //                                            x.IsDefault,
        //                                            x.ThumbnailImage
        //                                        }).FirstOrDefault()
        //                                }).Where(x => x.IsActive == true)
        //                                .Skip(Skip)
        //                                .Take(Take)
        //                                .ToListAsync();

        //    return new PackageDTable
        //    {
        //        Total = totalCount,
        //        PackageEntities = [..packages.Select(x => new PackageEntity()
        //        {
        //            Id= x.Id,
        //            Price = x.Price,
        //            ShortDescription = x.ShortDescription,
        //            Source = x.Source,
        //            Destination = x.Destination,
        //            FullDescription = x.FullDescription,
        //            Title = x.Title,
        //            TurmsandConditions=x.TurmsandConditions,
        //            DurationDays = x.DurationDays,
        //            PackageMedia =x.PackageMedia ==null?null:
        //            new  PackageMediaEntity(){
        //                MediaType=x.PackageMedia.MediaType,
        //                MediaUrl=x.PackageMedia.MediaUrl,
        //                ThumbnailImage=x.PackageMedia.ThumbnailImage
        //            }
        //        })],
        //        Filtered = totalCount
        //    };
        //}
    }
}

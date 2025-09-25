using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class PackageMediaRepository(BookingCmsContext context) : IPackageMediaRepository
    {
        private readonly BookingCmsContext _context = context;
        public async Task<int> DeletePackageMedia(int MediaId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeletePackageMediaByPackageId(int PackageId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PackageMediaEntity>> GetPackageMediaByPackageId(int PackageId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SavePackageMedia(PackageMediaEntity mediaEntity, CancellationToken token)
        {
            await _context.TourPackageMedia.AddAsync(new Data.Models.TourPackageMedium()
            {
                PackageId = mediaEntity.PackageId,
                MediaType = mediaEntity.MediaType,
                MediaUrl = mediaEntity.MediaUrl ?? string.Empty,
                ThumbnailUrl = mediaEntity.ThumbnailImage,
                CreatedBy = mediaEntity.CreatedBy,
                CreatedOn = mediaEntity.CreatedAt ?? DateTime.UtcNow,
                Caption = mediaEntity.Filename,
                UpdatedBy = mediaEntity.UpdatedBy,
                UpdatedOn = mediaEntity.UpdatedAt,
            }, token);
            return await _context.SaveChangesAsync(token);
        }

        public async Task<int> SavePackageMediaList(IEnumerable<PackageMediaEntity> mediaEntitys, CancellationToken token)
        {
           await _context.TourPackageMedia.AddRangeAsync(mediaEntitys.Select(mediaEntity => new Data.Models.TourPackageMedium()
            {
                PackageId = mediaEntity.PackageId,
                MediaType = mediaEntity.MediaType,
                MediaUrl = mediaEntity.MediaUrl ?? string.Empty,
                ThumbnailUrl = mediaEntity.ThumbnailImage,
                CreatedBy = mediaEntity.CreatedBy,
                CreatedOn = mediaEntity.CreatedAt ?? DateTime.UtcNow,
                Caption = mediaEntity.Filename,
                UpdatedBy = mediaEntity.UpdatedBy,
                UpdatedOn = mediaEntity.UpdatedAt,
            }), token);
            return await _context.SaveChangesAsync(token);
        }
    }
}

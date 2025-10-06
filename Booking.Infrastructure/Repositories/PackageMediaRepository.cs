using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class PackageMediaRepository(BookingCmsContext context) : IPackageMediaRepository
    {
        private readonly BookingCmsContext _context = context;
        public async Task<int> DeletePackageMedia(int MediaId, CancellationToken token)
        {
            return await _context.TourPackageMedia.Where(u => u.MediaId == MediaId).ExecuteDeleteAsync(token);
        }

        public async Task<int> DeletePackageMediaByPackageId(int PackageId, CancellationToken token)
        {
            return await _context.TourPackageMedia.Where(u => u.PackageId == PackageId).ExecuteDeleteAsync(token);
        }

        public async Task<IEnumerable<PackageMediaEntity>> GetPackageMediaByPackageId(int PackageId, CancellationToken token)
        {
            return await _context.TourPackageMedia.Where(u => u.PackageId == PackageId).Select(x => new PackageMediaEntity()
            {
                PackageId = x.PackageId,
                MediaType = x.MediaType,
                MediaUrl = x.MediaUrl,
                ThumbnailImage = x.ThumbnailUrl ?? string.Empty,
                Filename = x.Caption,
                Id = x.MediaId,
                UpdatedAt = x.UpdatedOn
            }).ToListAsync(cancellationToken: token);
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
                CreatedOn = mediaEntity.CreatedAt,
                Caption = mediaEntity.Filename,
                UpdatedBy = mediaEntity.UpdatedBy,
                UpdatedOn = mediaEntity.UpdatedAt,
            }, token);
            return await _context.SaveChangesAsync(token);
        }
        public async Task<int> UpdatePackageMedia(PackageMediaEntity mediaEntity, CancellationToken token)
        {
            return await _context.TourPackageMedia
                            .Where(x => x.MediaId.Equals(mediaEntity.Id))
                            .ExecuteUpdateAsync(c => c
                                .SetProperty(s => s.Caption, mediaEntity.Filename)
                                .SetProperty(s => s.ThumbnailUrl, mediaEntity.ThumbnailImage)
                                .SetProperty(s => s.MediaUrl, mediaEntity.MediaUrl)
                                .SetProperty(s => s.MediaType, mediaEntity.MediaType)
                                .SetProperty(s => s.UpdatedOn, mediaEntity.UpdatedAt)
                                .SetProperty(s => s.UpdatedBy, mediaEntity.UpdatedBy)
                            , token);

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
                CreatedOn = mediaEntity.CreatedAt,
                Caption = mediaEntity.Filename,
                UpdatedBy = mediaEntity.UpdatedBy,
                UpdatedOn = mediaEntity.UpdatedAt,
            }), token);
            return await _context.SaveChangesAsync(token);
        }
    }
}

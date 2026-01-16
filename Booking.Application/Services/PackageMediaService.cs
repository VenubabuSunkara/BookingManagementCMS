using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;
using System.Collections.Immutable;

namespace Booking.Application.Services
{
    public class PackageMediaService(IPackageMediaRepository packageMediaRepository) : IPackageMediaService
    {
        private readonly IPackageMediaRepository _packageMediaRepository = packageMediaRepository;


        public async Task<int> DeletePackageMedia(int MediaId, CancellationToken token)
        {
            return await _packageMediaRepository.DeletePackageMedia(MediaId, token);
        }

        public async Task<int> DeletePackageMediaByPackageId(int PackageId, CancellationToken token)
        {
            return await _packageMediaRepository.DeletePackageMediaByPackageId(PackageId, token);
        }

        public async Task<IEnumerable<PackageMediaDto>> GetPackageMediaByPackageId(int PackageId, CancellationToken token)
        {
            var packageMedia = await GetPackageMediaByPackageId(PackageId, token);
            return [.. packageMedia.Select(x => new PackageMediaDto()
            {
                MediaType = x.MediaType,
                MediaUrl = x.MediaUrl,
                PackageId = PackageId,
                ThumbnailImage = x.ThumbnailImage,
                FileName = x.FileName,
                CreatedAt = x.CreatedAt,
            })];
        }

        public async Task<int> SavePackageMedia(PackageMediaDto mediaEntity, CancellationToken token)
        {
            return await _packageMediaRepository.SavePackageMedia(
                new PackageMediaEntity()
                {
                    PackageId = mediaEntity.PackageId,
                    MediaType = mediaEntity.MediaType,
                    MediaUrl = mediaEntity.MediaUrl,
                    ThumbnailImage = mediaEntity.ThumbnailImage,
                    CreatedBy = mediaEntity.CreatedBy,
                    CreatedAt = mediaEntity.CreatedAt,
                    Filename = mediaEntity.FileName,
                    UpdatedBy = mediaEntity.UpdatedBy,
                    UpdatedAt = mediaEntity.UpdatedAt
                }, token);
        }

        public async Task<int> SavePackageMediaList(IEnumerable<PackageMediaDto> mediaEntitys, CancellationToken token)
        {
            return await _packageMediaRepository.SavePackageMediaList(mediaEntitys.Select(mediaEntity => new PackageMediaEntity()
            {
                PackageId = mediaEntity.PackageId,
                MediaType = mediaEntity.MediaType,
                MediaUrl = mediaEntity.MediaUrl,
                ThumbnailImage = mediaEntity.ThumbnailImage,
                CreatedBy = mediaEntity.CreatedBy,
                CreatedAt = mediaEntity.CreatedAt,
                Filename = mediaEntity.FileName,
                UpdatedBy = mediaEntity.UpdatedBy,
                UpdatedAt = mediaEntity.UpdatedAt
            }), token);
        }

        public async Task<int> UpdatePackageMedia(PackageMediaDto mediaEntity, CancellationToken token)
        {
            return await _packageMediaRepository.UpdatePackageMedia(new PackageMediaEntity()
            {
                Filename = mediaEntity.FileName,
                MediaType = mediaEntity.MediaType,
                MediaUrl = mediaEntity.MediaUrl,
                IsDefault = mediaEntity.IsDefault,
                ThumbnailImage = mediaEntity.ThumbnailImage,
                Id = mediaEntity.Id,
                PackageId = mediaEntity.PackageId,
                UpdatedAt = mediaEntity.UpdatedAt,
                UpdatedBy = mediaEntity.UpdatedBy,
            }, token);
        }
    }
}

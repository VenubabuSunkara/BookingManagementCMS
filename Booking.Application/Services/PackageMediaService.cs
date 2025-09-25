using Booking.Application.DTOs.Tour;
using Booking.Application.Interfaces;
using Booking.Domain.Entities.Tour;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class PackageMediaService(IPackageMediaRepository packageMediaRepository) : IPackageMediaService
    {
        private readonly IPackageMediaRepository _packageMediaRepository = packageMediaRepository;


        public Task<int> DeletePackageMedia(int MediaId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletePackageMediaByPackageId(int PackageId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PackageMediaDto>> GetPackageMediaByPackageId(int PackageId, CancellationToken token)
        {
            throw new NotImplementedException();
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
    }
}

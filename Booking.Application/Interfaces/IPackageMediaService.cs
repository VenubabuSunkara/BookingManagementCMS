using Booking.Application.DTOs.Tour;
using Booking.Domain.Entities.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface IPackageMediaService
    {
        Task<int> SavePackageMedia(PackageMediaDto mediaEntity, CancellationToken token);
        Task<int> SavePackageMediaList(IEnumerable<PackageMediaDto> mediaEntitys, CancellationToken token);
        Task<int> DeletePackageMedia(int MediaId, CancellationToken token);
        Task<int> DeletePackageMediaByPackageId(int PackageId, CancellationToken token);
        Task<IEnumerable<PackageMediaDto>> GetPackageMediaByPackageId(int PackageId, CancellationToken token);

    }
}

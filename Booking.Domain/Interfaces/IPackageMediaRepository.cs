using Booking.Domain.Entities.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IPackageMediaRepository
    {
        Task<int> SavePackageMedia(PackageMediaEntity mediaEntity, CancellationToken token);
        Task<int> DeletePackageMedia(int MediaId, CancellationToken token);
        Task<int> DeletePackageMediaByPackageId(int PackageId, CancellationToken token);
        Task<int> SavePackageMediaList(IEnumerable<PackageMediaEntity> mediaEntitys, CancellationToken token);
        Task<int> UpdatePackageMedia(PackageMediaEntity mediaEntity, CancellationToken token);
        Task<IEnumerable<PackageMediaEntity>> GetPackageMediaByPackageId(int PackageId, CancellationToken token);
    }
}

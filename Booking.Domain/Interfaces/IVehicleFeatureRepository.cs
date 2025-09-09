using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IVehicleFeatureRepository
    {
        Task<IEnumerable<FeatureEntity>?> GetVehicleFeaturesListAsync(int VehicleId, CancellationToken token);
        Task<int> AddFeatureAsync(FeatureEntity entity, CancellationToken token);
        Task<int> UpdateFeatureAsync(FeatureEntity entity, CancellationToken token);
        Task<int> DeleteFeatureAsync(FeatureEntity entity, CancellationToken token);

    }
}

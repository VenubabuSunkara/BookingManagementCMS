using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class VehicleFeatureRepository : IVehicleFeatureRepository
    {
        public Task<int> AddFeatureAsync(FeatureEntity entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteFeatureAsync(FeatureEntity entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FeatureEntity>?> GetVehicleFeaturesListAsync(int VehicleId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateFeatureAsync(FeatureEntity entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}

using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IReviewCommentsRepository
    {
        /// <summary>
        /// Get All Review comments
        /// </summary>
        /// <param name="DriverId"></param>
        /// <returns></returns>
        Task<IEnumerable<ReviewCommentEntity>> GetAllVehicleDriverReviewsAsync(int DriverId, int VehicleId, CancellationToken token);
        Task<ReviewCommentTableEntity> GetAllReviewComments(string Search, int Take, int Skip, CancellationToken token);
    }
}

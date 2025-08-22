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
        Task<IEnumerable<ReviewCommentEntity>> GetAllAsync(int DriverId);
    }
}

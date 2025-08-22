using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class ReviewCommentsRepository(BookingCmsContext context) : IReviewCommentsRepository
    {
        /// <summary>
        /// Database  Context
        /// </summary>
        private readonly BookingCmsContext _context = context;
        /// <summary>
        /// Get All Review Comments by Driver
        /// </summary>
        /// <param name="DriverId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ReviewCommentEntity>> GetAllAsync(int DriverId)
        {
            var reviewComments = await _context.ReviewComments.Where(x => x.DriverId.Equals(DriverId)).ToListAsync();
            return reviewComments.Select(x => new ReviewCommentEntity()
            {
                Comment = x.Comment,
                DriverId = x.DriverId,
                Rating = x.Rating,
                Id = x.Id,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                UpdatedBy = x.UpdatedBy,
                UpdatedOn = x.UpdatedOn
            }).AsParallel();
        }
    }
}

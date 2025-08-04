using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class ReviewCommentService(IReviewCommentsRepository reviewCommentsRepository) : IReviewCommentService
    {
        /// <summary>
        /// Repository
        /// </summary>
        private readonly IReviewCommentsRepository _reviewCommentsRepository = reviewCommentsRepository;

        /// <summary>
        /// Get Driver vehicle Comments
        /// </summary>
        /// <param name="DriverId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ReviewCommentsDto>> GetAllAsync(int DriverId)
        {
            var reviewComments = await _reviewCommentsRepository.GetAllAsync(DriverId);
            return reviewComments.Select(x => new ReviewCommentsDto()
            {
                Comment = x.Comment,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                DriverId = x.DriverId,
                Id = x.Id,
                Rating = x.Rating,
                UpdatedBy = x.UpdatedBy,
                UpdatedOn = x.UpdatedOn,

            }).AsParallel();
        }
    }
}

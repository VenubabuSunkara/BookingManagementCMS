using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class ReviewCommentService(IReviewCommentsRepository reviewCommentsRepository) : IReviewCommentService
    {
        /// <summary>
        /// Repository
        /// </summary>
        private readonly IReviewCommentsRepository _reviewCommentsRepository = reviewCommentsRepository;

        public async Task<ReviewCommentTableDto> GetAllReviewComments(string Search, int Take, int Skip, CancellationToken token)
        {
            var reviewComments = await _reviewCommentsRepository.GetAllReviewComments(Search, Take, Skip, token);
            return new ReviewCommentTableDto()
            {
                Total = reviewComments.Total,
                Filtered = reviewComments.Filtered,
                ReviewComments = reviewComments.ReviewComments.Select(x => new ReviewCommentsDto()
                {
                    DriverComment = x.DriverComment,
                    DriverId = x.DriverId,
                    VehicleId = x.VehicleId,
                    Id = x.Id,
                    Rating = x.Rating,
                    VehicleComment = x.VehicleComment,
                    Suggestions = x.Suggestions,
                    DriverLicense = x.DriverLicense,
                    VehicleNo = x.VehicleNo,
                    CreatedOn = x.CreatedOn,
                })
            };

        }

        public async Task<IEnumerable<ReviewCommentsDto>> GetAllVehicleDriverReviewsAsync(int DriverId, int VehicleId, CancellationToken token)
        {
            var reviewComments = await _reviewCommentsRepository.GetAllVehicleDriverReviewsAsync(DriverId, VehicleId, token);
            return reviewComments.Select(x => new ReviewCommentsDto()
            {
                DriverComment = x.DriverComment,
                DriverId = x.DriverId,
                VehicleId = x.VehicleId,
                Id = x.Id,
                Rating = x.Rating,
                VehicleComment = x.VehicleComment,
                Suggestions = x.Suggestions,
                CreatedOn = x.CreatedOn,
            });
        }
    }
}

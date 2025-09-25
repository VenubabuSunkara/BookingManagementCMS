using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class ReviewCommentsRepository(BookingCmsContext context) : IReviewCommentsRepository
    {
        /// <summary>
        /// Database  Context
        /// </summary>
        private readonly BookingCmsContext _context = context;

        public async Task<ReviewCommentTableEntity> GetAllReviewComments(string Search, int Take, int Skip, CancellationToken token)
        {
            var q = _context.ReviewComments
                .AsNoTracking();
            var total = await q.CountAsync(token);

            if (!string.IsNullOrWhiteSpace(Search))
            {
                q = q.Where(d =>
                            //d.DriverComment.Contains(Search) ||
                            //d.VehicleComment.Contains(Search) ||
                            //d.Driver.LicenseNumber.Contains(Search) ||
                            //d.Vehicle.VehicleNumber.Contains(Search) ||
                            d.Rating.ToString().Contains(Search));
            }
            // simple order by FullName default
            q = q.OrderByDescending(d => d.CreatedOn);

            var filtered = await q.CountAsync(token);
            var page = await q.Skip(Skip).Take(Take).ToListAsync(token);

            return new ReviewCommentTableEntity
            {
                Total = total,
                Filtered = filtered,
                ReviewComments = [.. page.Select(x => new ReviewCommentEntity()
                   {
                       DriverId = x.DriverId,
                       //VehicleId = x.VehicleId,
                       //VehicleNo=x.Vehicle?.VehicleNumber,
                       //DriverLicense=x.Driver?.LicenseNumber,
                       //DriverComment = x.DriverComment,
                       //VehicleComment = x.VehicleComment,
                       //Suggestions = x.Suggestion,
                       CreatedOn = x.CreatedOn,
                       Id = x.Id,
                       Rating = x.Rating
                   })]
            };

        }

        /// <summary>
        /// Get All Review Comments by Driver
        /// </summary>
        /// <param name="DriverId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ReviewCommentEntity>> GetAllVehicleDriverReviewsAsync(int DriverId, int VehicleId, CancellationToken token)
        {
            return await _context.ReviewComments
                .Where(x => x.DriverId.Equals(DriverId))
                .Select(x => new ReviewCommentEntity()
                {
                    DriverId = x.DriverId,
                    //VehicleId = x.VehicleId,
                    //DriverComment = x.DriverComment,
                    //VehicleComment = x.VehicleComment,
                    //Suggestions = x.Suggestion,
                    CreatedOn = x.CreatedOn,
                    Id = x.Id,
                    Rating = x.Rating
                }).ToListAsync(token);
        }
    }
}

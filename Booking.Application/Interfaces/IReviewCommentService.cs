using Booking.Application.DTOs;
using Booking.Domain.Entities;

namespace Booking.Application.Interfaces
{
    public interface IReviewCommentService
    {
        Task<IEnumerable<ReviewCommentsDto>> GetAllVehicleDriverReviewsAsync(int DriverId,int VehicleId, CancellationToken token);
        Task<ReviewCommentTableDto> GetAllReviewComments(string Search, int Take, int Skip, CancellationToken token);

    }
}

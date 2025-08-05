using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces;

public interface ICouponCodeRepository
{
    Task<bool> CreateCouponCodeAsync(CouponCode couponCode, CancellationToken cancellationToken);
    Task<bool> UpdateCouponCodeAsync(CouponCode couponCode, CancellationToken cancellationToken);
    Task<bool> DeleteCouponCodeAsync(int couponCodeId, CancellationToken cancellationToken);
    Task<bool> FindCouponCodeAsync(int couponCodeId, CancellationToken cancellationToken);
    Task<IEnumerable<CouponCode>> GetAllCouponCodesAsync(CancellationToken cancellationToken);
    Task<CouponCode?> GetCouponCodeByIdAsync(int couponCodeId, CancellationToken cancellationToken);
    IQueryable<CouponCode> GetQuarableCouponCodeData();
}

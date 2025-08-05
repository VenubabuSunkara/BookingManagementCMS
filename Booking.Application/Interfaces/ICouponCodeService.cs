using Booking.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces;

public interface ICouponCodeService
{
    Task<bool> CreateCouponCodeAsync(CouponCodeDto couponCodeDto, CancellationToken cancellationToken);
    Task<bool> UpdateCouponCodeAsync(CouponCodeDto couponCodeDto, CancellationToken cancellationToken);
    Task<bool> DeleteCouponCodeAsync(int couponCodeId, CancellationToken cancellationToken);
    Task<bool> FindCouponCodeAsync(int couponCodeId, CancellationToken cancellationToken);
    Task<IEnumerable<CouponCodeDto>> GetAllCouponCodesAsync(CancellationToken cancellationToken);
    Task<CouponCodeDto?> GetCouponCodeByIdAsync(int couponCodeId, CancellationToken cancellationToken);
    IQueryable<CouponCodeDto> GetQuarableCouponCodeData();
}

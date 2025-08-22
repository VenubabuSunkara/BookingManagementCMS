using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces;

public interface ICouponCodeRepository
{
    Task<bool> CreateCouponCodeAsync(CouponCodeEntity couponCode, CancellationToken cancellationToken);
    Task<bool> UpdateCouponCodeAsync(CouponCodeEntity couponCode, CancellationToken cancellationToken);
    Task<bool> DeleteCouponCodeAsync(int couponCodeId, CancellationToken cancellationToken);
    Task<bool> FindCouponCodeAsync([Optional]int couponCodeId, [Optional] string couponCode, CancellationToken cancellationToken = default);
    Task<CouponCodeDataTableEntity> GetCouponCodeListAsync(int Skip, int Take, string searchKey, CancellationToken cancellationToken);
    Task<IEnumerable<CouponCodeExportEntity>> ExportAllAsync();
    Task<CouponCodeEntity?> GetCouponCodeByIdAsync(int couponCodeId, CancellationToken cancellationToken);
    IQueryable<CouponCodeEntity> GetQuarableCouponCodeData();
}

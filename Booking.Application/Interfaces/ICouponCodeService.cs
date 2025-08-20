using Booking.Application.DTOs;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces;

public interface ICouponCodeService
{
    Task<bool> CreateCouponCodeAsync(CouponCodeDto couponCodeDto, CancellationToken cancellationToken);
    Task<bool> UpdateCouponCodeAsync(CouponCodeDto couponCodeDto, CancellationToken cancellationToken);
    Task<bool> DeleteCouponCodeAsync(int couponCodeId, CancellationToken cancellationToken);
    Task<bool> FindCouponCodeAsync([Optional] int couponCodeId, [Optional] string couponCode, CancellationToken cancellationToken = default);
    Task<CouponCodeDataTableDto> GetCouponCodeListAsync(int Skip, int Take, string searchKey, CancellationToken cancellationToken);
    Task<IEnumerable<CouponCodeExporDto>> ExportAllAsync();
    Task<CouponCodeDto?> GetCouponCodeByIdAsync(int couponCodeId, CancellationToken cancellationToken);
    IQueryable<CouponCodeDto> GetQuarableCouponCodeData();
}

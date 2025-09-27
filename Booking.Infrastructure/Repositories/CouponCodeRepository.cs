using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using static Azure.Core.HttpHeader;

namespace Booking.Infrastructure.Repositories;

public sealed class CouponCodeRepository(BookingCmsContext context) : ICouponCodeRepository
{
    private readonly BookingCmsContext _context = context;

    /// <summary>
    /// Create new CouponCode
    /// </summary>
    /// <param name="couponCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> CreateCouponCodeAsync(CouponCodeEntity couponCode, CancellationToken cancellationToken)
    {
        await _context.CouponCodes.AddAsync(new()
        {
            Code = couponCode.Code,
            StartDate = couponCode.StartDate,
            EndDate = couponCode.EndDate,
            DiscountType = couponCode.DiscountType,
            DiscountValue = couponCode.DiscountValue,
            CreatedOn = couponCode.CreatedOn,
            UpdatedOn = couponCode.UpdatedOn,
            CreatedBy = couponCode.CreatedBy,
            UpdatedBy = couponCode.UpdatedBy,
            Description = couponCode.Description,
            UsageCount = couponCode.UsageCount,
            MaximumDiscount = couponCode.MaximumDiscount,
            MinimumAmount = couponCode.MinimumAmount,
            UsageLimit = couponCode.UsageLimit,
            IsActive = couponCode.IsActive
        }, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    /// <summary>
    /// Update couponcode
    /// </summary>
    /// <param name="couponCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> UpdateCouponCodeAsync(CouponCodeEntity couponCode, CancellationToken cancellationToken)
    {
        return await _context.CouponCodes
                             .Where(x => x.Id.Equals(couponCode.Id))
                             .ExecuteUpdateAsync(c => c
                                 .SetProperty(s => s.Code, couponCode.Code)
                                 .SetProperty(s => s.StartDate, couponCode.StartDate)
                                 .SetProperty(s => s.EndDate, couponCode.EndDate)
                                 .SetProperty(s => s.DiscountType, couponCode.DiscountType)
                                 .SetProperty(s => s.DiscountValue, couponCode.DiscountValue)
                                 .SetProperty(s => s.Description, couponCode.Description)
                                 .SetProperty(s => s.UsageCount, couponCode.UsageCount)
                                 .SetProperty(s => s.MaximumDiscount, couponCode.MaximumDiscount)
                                 .SetProperty(s => s.MinimumAmount, couponCode.MinimumAmount)
                                 .SetProperty(s => s.UpdatedOn, couponCode.UpdatedOn)
                                 .SetProperty(s => s.UpdatedBy, couponCode.UpdatedBy)
                             , cancellationToken) > 0;
    }

    /// <summary>
    /// Delete couponcode
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> DeleteCouponCodeAsync(int couponCodeId, CancellationToken cancellationToken)
    {
        return await _context.CouponCodes.Where(x => x.Id.Equals(couponCodeId)).ExecuteDeleteAsync(cancellationToken) > 0;
    }

    /// <summary>
    /// Check the coupon code is existing
    /// </summary>
    /// <param name="couponCodeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> FindCouponCodeAsync([Optional] int couponCodeId, [Optional] string couponCode, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(couponCode)) return false;

        Expression<Func<CouponCode, bool>> expression = x => couponCodeId > 0 ? x.Id.Equals(couponCodeId) && x.Code.Equals(couponCode)
                                                                              : x.Code.Equals(couponCode);

        return await _context.CouponCodes.AnyAsync(expression, cancellationToken);
    }

    /// <summary>
    /// Get all the coupon codes
    /// </summary>
    /// <param name="Skip"></param>
    /// <param name="Take"></param>
    /// <param name="searchKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CouponCodeDataTableEntity> GetCouponCodeListAsync(int Skip, int Take, string searchKey, CancellationToken cancellationToken)
    {
        // Base query (read-only, optimized)
        var query = _context.CouponCodes.AsNoTracking();
        var total = await query.CountAsync(cancellationToken);
        // Apply search if provided
        if (!string.IsNullOrWhiteSpace(searchKey))
        {
            query = query.Where(c =>
                c.Code!.Contains(searchKey) ||
                (c.DiscountType ?? string.Empty).Contains(searchKey) ||
                Convert.ToString(c.StartDate)!.Contains(searchKey) ||
                Convert.ToString(c.EndDate)!.Contains(searchKey))
                .Where(x => x.IsActive == true);
        }
        //Total Count
        var filterCount = await query.CountAsync(cancellationToken);
        var couponCodeList = await query.Select(coupon => new CouponCodeEntity()
        {
            Code = coupon.Code,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate,
            DiscountType = coupon.DiscountType,
            DiscountValue = coupon.DiscountValue,
            CreatedOn = coupon.CreatedOn,
            UsageCount = coupon.UsageCount,
            MaximumDiscount = coupon.MaximumDiscount,
            MinimumAmount = coupon.MinimumAmount,
            UsageLimit = coupon.UsageLimit,
        }).Skip(Skip).Take(Take).ToListAsync(cancellationToken);

        //Final Result
        return new()
        {
            Total = total,
            Filtered = filterCount,
            CouponCode = couponCodeList
        };
    }

    /// <summary>
    /// Get coupon code export results
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<CouponCodeExportEntity>> ExportAllAsync()
    {
        return await _context.CouponCodes.AsNoTracking()
            .Select(coupon => new CouponCodeExportEntity()
            {
                Code = coupon.Code,
                StartDate = coupon.StartDate,
                EndDate = coupon.EndDate,
                DiscountType = coupon.DiscountType,
                DiscountValue = coupon.DiscountValue,
                CreatedOn = coupon.CreatedOn,
                UpdatedOn = coupon.UpdatedOn,
                CreatedBy = coupon.CreatedBy,
                UpdatedBy = coupon.UpdatedBy,
                Description = coupon.Description,
                UsageCount = coupon.UsageCount,
                MaximumDiscount = coupon.MaximumDiscount,
                MinimumAmount = coupon.MinimumAmount,
                UsageLimit = coupon.UsageLimit,
                IsActive = coupon.IsActive
            }).ToListAsync();
    }

    /// <summary>
    /// Get the single couponcode information
    /// </summary>
    /// <param name="couponCodeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CouponCodeEntity?> GetCouponCodeByIdAsync(int couponCodeId, CancellationToken cancellationToken)
    {
        return await _context.CouponCodes
            .Where(coupon => coupon.Id.Equals(couponCodeId))
            .Select(coupon => new CouponCodeEntity()
            {
                Code = coupon.Code,
                StartDate = coupon.StartDate,
                EndDate = coupon.EndDate,
                DiscountType = coupon.DiscountType,
                DiscountValue = coupon.DiscountValue,
                CreatedOn = coupon.CreatedOn,
                UpdatedOn = coupon.UpdatedOn,
                CreatedBy = coupon.CreatedBy,
                UpdatedBy = coupon.UpdatedBy,
                Description = coupon.Description,
                UsageCount = coupon.UsageCount,
                MaximumDiscount = coupon.MaximumDiscount,
                MinimumAmount = coupon.MinimumAmount,
                UsageLimit = coupon.UsageLimit,
                IsActive = coupon.IsActive ?? false
            }).FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Get the quarable couponcodes
    /// </summary>
    /// <returns></returns>
    public IQueryable<CouponCodeEntity> GetQuarableCouponCodeData()
    {
        return _context.CouponCodes
            .Select(coupon => new CouponCodeEntity()
            {
                Code = coupon.Code,
                StartDate = coupon.StartDate,
                EndDate = coupon.EndDate,
                DiscountType = coupon.DiscountType,
                DiscountValue = coupon.DiscountValue,
                CreatedOn = coupon.CreatedOn,
                UpdatedOn = coupon.UpdatedOn,
                CreatedBy = coupon.CreatedBy,
                UpdatedBy = coupon.UpdatedBy,
                Description = coupon.Description,
                UsageCount = coupon.UsageCount,
                MaximumDiscount = coupon.MaximumDiscount,
                MinimumAmount = coupon.MinimumAmount,
                UsageLimit = coupon.UsageLimit,
                IsActive = coupon.IsActive ?? false
            }).AsQueryable();
    }
}

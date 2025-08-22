using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

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
            ValidityFrom = couponCode.ValidityFrom,
            ValidityTo = couponCode.ValidityTo,
            PriceRangeMin = couponCode.PriceRangeMin,
            PriceRangeMax = couponCode.PriceRangeMax,
            DiscountType = couponCode.DiscountType,
            DiscountValue = couponCode.DiscountValue,
            CreatedOn = couponCode.CreatedOn,
            UpdatedOn = couponCode.UpdatedOn,
            CreatedBy = couponCode.CreatedBy,
            UpdatedBy = couponCode.UpdatedBy,
            MediaUrl = couponCode.MediaUrl
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
                                 .SetProperty(s => s.ValidityFrom, couponCode.ValidityFrom)
                                 .SetProperty(s => s.ValidityTo, couponCode.ValidityTo)
                                 .SetProperty(s => s.PriceRangeMin, couponCode.PriceRangeMin)
                                 .SetProperty(s => s.PriceRangeMax, couponCode.PriceRangeMax)
                                 .SetProperty(s => s.DiscountType, couponCode.DiscountType)
                                 .SetProperty(s => s.DiscountValue, couponCode.DiscountValue)
                                 .SetProperty(s => s.UpdatedOn, couponCode.UpdatedOn)
                                 .SetProperty(s => s.UpdatedBy, couponCode.UpdatedBy)
                                 .SetProperty(s => s.MediaUrl, couponCode.MediaUrl)
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
    /// Check the couponcode existance
    /// </summary>
    /// <param name="couponCodeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> FindCouponCodeAsync([Optional] int couponCodeId, [Optional] string couponCode, CancellationToken cancellationToken = default)
    {
        if(string.IsNullOrEmpty(couponCode)) return false;

        Expression<Func<CouponCode, bool>> expression = x => couponCodeId > 0 ? x.Id.Equals(couponCodeId) && x.Code.Equals(couponCode)
                                                                              : x.Code.Equals(couponCode);

        return await _context.CouponCodes.AnyAsync(expression, cancellationToken);
    }

    /// <summary>
    /// Get all the couponcodes
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CouponCodeEntity>> GetAllCouponCodesAsync(CancellationToken cancellationToken)
    {
        var couponCodeList = await _context.CouponCodes.AsNoTracking().ToListAsync(cancellationToken);

        return couponCodeList.Select(s => new CouponCodeEntity()
        {
            Id = s.Id,
            Code = s.Code,
            ValidityFrom = s.ValidityFrom,
            ValidityTo = s.ValidityTo,
            PriceRangeMin = s.PriceRangeMin,
            PriceRangeMax = s.PriceRangeMax,
            DiscountType = s.DiscountType,
            DiscountValue = s.DiscountValue,
            CreatedOn = s.CreatedOn,
            UpdatedOn = s.UpdatedOn,
            CreatedBy = s.CreatedBy,
            UpdatedBy = s.UpdatedBy,
            MediaUrl = s.MediaUrl
        }).AsParallel();
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
            .Where(x => x.Id.Equals(couponCodeId))
            .Select(s => new CouponCodeEntity()
            {
                Id = s.Id,
                Code = s.Code,
                ValidityFrom = s.ValidityFrom,
                ValidityTo = s.ValidityTo,
                PriceRangeMin = s.PriceRangeMin,
                PriceRangeMax = s.PriceRangeMax,
                DiscountType = s.DiscountType,
                DiscountValue = s.DiscountValue,
                CreatedOn = s.CreatedOn,
                UpdatedOn = s.UpdatedOn,
                CreatedBy = s.CreatedBy,
                UpdatedBy = s.UpdatedBy,
                MediaUrl = s.MediaUrl
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Get the quarable couponcodes
    /// </summary>
    /// <returns></returns>
    public IQueryable<CouponCodeEntity> GetQuarableCouponCodeData()
    {
        return _context.CouponCodes
            .Select(s => new CouponCodeEntity()
            {
                Id = s.Id,
                Code = s.Code,
                ValidityFrom = s.ValidityFrom,
                ValidityTo = s.ValidityTo,
                PriceRangeMin = s.PriceRangeMin,
                PriceRangeMax = s.PriceRangeMax,
                DiscountType = s.DiscountType,
                DiscountValue = s.DiscountValue,
                CreatedOn = s.CreatedOn,
                UpdatedOn = s.UpdatedOn,
                CreatedBy = s.CreatedBy,
                UpdatedBy = s.UpdatedBy,
                MediaUrl = s.MediaUrl
            }).AsParallel().AsQueryable();
    }
}

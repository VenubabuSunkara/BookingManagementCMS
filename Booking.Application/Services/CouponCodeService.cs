using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services;

public sealed class CouponCodeService(ICouponCodeRepository couponCodeRepository) : ICouponCodeService
{
    private readonly ICouponCodeRepository _couponCodeRepository = couponCodeRepository;
    /// <summary>
    /// Create new CouponCode
    /// </summary>
    /// <param name="couponCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> CreateCouponCodeAsync(CouponCodeDto couponCodeDto, CancellationToken cancellationToken)
    {
        return await _couponCodeRepository.CreateCouponCodeAsync(new()
        {
            Code = couponCodeDto.Code,
            ValidityFrom = couponCodeDto.ValidityFrom,
            ValidityTo = couponCodeDto.ValidityTo,
            PriceRangeMin = couponCodeDto.PriceRangeMin,
            PriceRangeMax = couponCodeDto.PriceRangeMax,
            DiscountType = couponCodeDto.DiscountType,
            DiscountValue = couponCodeDto.DiscountValue,
            CreatedOn = couponCodeDto.CreatedOn,
            UpdatedOn = couponCodeDto.UpdatedOn,
            CreatedBy = couponCodeDto.CreatedBy,
            UpdatedBy = couponCodeDto.UpdatedBy,
            MediaUrl = couponCodeDto.MediaUrl
        }, cancellationToken);
    }

    /// <summary>
    /// Update couponcode
    /// </summary>
    /// <param name="couponCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> UpdateCouponCodeAsync(CouponCodeDto couponCodeDto, CancellationToken cancellationToken)
    {
        return await _couponCodeRepository.UpdateCouponCodeAsync(new()
        {
            Id = couponCodeDto.Id,
            Code = couponCodeDto.Code,
            ValidityFrom = couponCodeDto.ValidityFrom,
            ValidityTo = couponCodeDto.ValidityTo,
            PriceRangeMin = couponCodeDto.PriceRangeMin,
            PriceRangeMax = couponCodeDto.PriceRangeMax,
            DiscountType = couponCodeDto.DiscountType,
            DiscountValue = couponCodeDto.DiscountValue,
            CreatedOn = couponCodeDto.CreatedOn,
            UpdatedOn = couponCodeDto.UpdatedOn,
            CreatedBy = couponCodeDto.CreatedBy,
            UpdatedBy = couponCodeDto.UpdatedBy,
            MediaUrl = couponCodeDto.MediaUrl
        }, cancellationToken);
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
        return await _couponCodeRepository.DeleteCouponCodeAsync(couponCodeId, cancellationToken);
    }

    /// <summary>
    /// Check the couponcode existance
    /// </summary>
    /// <param name="couponCodeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> FindCouponCodeAsync(int couponCodeId, CancellationToken cancellationToken)
    {
        return await _couponCodeRepository.FindCouponCodeAsync(couponCodeId, cancellationToken);
    }

    /// <summary>
    /// Get all the couponcodes
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CouponCodeDto>> GetAllCouponCodesAsync(CancellationToken cancellationToken)
    {
        var couponCodeList = await _couponCodeRepository.GetAllCouponCodesAsync(cancellationToken);

        return couponCodeList.Select(s => new CouponCodeDto()
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
        }).AsParallel().ToList();
    }

    /// <summary>
    /// Get the single couponcode information
    /// </summary>
    /// <param name="couponCodeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CouponCodeDto?> GetCouponCodeByIdAsync(int couponCodeId, CancellationToken cancellationToken)
    {
        var couponCode = await _couponCodeRepository.GetCouponCodeByIdAsync(couponCodeId, cancellationToken);

        if (couponCode == null) throw new NullReferenceException();

        return new()
        {
            Id = couponCode.Id,
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
        };
    }

    /// <summary>
    /// Get the quarable couponcodes
    /// </summary>
    /// <returns></returns>
    public IQueryable<CouponCodeDto> GetQuarableCouponCodeData()
    {
        var couponCodeQuarable = _couponCodeRepository.GetQuarableCouponCodeData();
        return couponCodeQuarable
            .Select(s => new CouponCodeDto()
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

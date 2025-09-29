using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;
using System.Runtime.InteropServices;
using static Azure.Core.HttpHeader;

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
            StartDate = couponCodeDto.StartDate,
            EndDate = couponCodeDto.EndDate,
            DiscountType = couponCodeDto.DiscountType,
            DiscountValue = couponCodeDto.DiscountValue,
            CreatedOn = couponCodeDto.CreatedOn,
            UpdatedOn = couponCodeDto.UpdatedOn,
            CreatedBy = couponCodeDto.CreatedBy,
            UpdatedBy = couponCodeDto.UpdatedBy,
            Description = couponCodeDto.Description,
            UsageCount = couponCodeDto.UsageCount,
            MaximumDiscount = couponCodeDto.MaximumDiscount,
            MinimumAmount = couponCodeDto.MinimumAmount,
            UsageLimit = couponCodeDto.UsageLimit,
            IsActive = couponCodeDto.IsActive
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
            Code = couponCodeDto.Code,
            StartDate = couponCodeDto.StartDate,
            EndDate = couponCodeDto.EndDate,
            DiscountType = couponCodeDto.DiscountType,
            DiscountValue = couponCodeDto.DiscountValue,
            CreatedOn = couponCodeDto.CreatedOn,
            UpdatedOn = couponCodeDto.UpdatedOn,
            CreatedBy = couponCodeDto.CreatedBy,
            UpdatedBy = couponCodeDto.UpdatedBy,
            Description = couponCodeDto.Description,
            UsageCount = couponCodeDto.UsageCount,
            MaximumDiscount = couponCodeDto.MaximumDiscount,
            MinimumAmount = couponCodeDto.MinimumAmount,
            UsageLimit = couponCodeDto.UsageLimit,
            IsActive = couponCodeDto.IsActive,
            Id=couponCodeDto.Id
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
    public async Task<bool> FindCouponCodeAsync([Optional] int couponCodeId, [Optional] string couponCode, CancellationToken cancellationToken = default)
    {
        return await _couponCodeRepository.FindCouponCodeAsync(couponCodeId, couponCode, cancellationToken);
    }

    /// <summary>
    /// Get all the couponcodes
    /// </summary>
    /// <param name="Skip"></param>
    /// <param name="Take"></param>
    /// <param name="searchKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CouponCodeDataTableDto> GetCouponCodeListAsync(int Skip, int Take, string searchKey, CancellationToken cancellationToken)
    {
        var couponCodeList = await _couponCodeRepository.GetCouponCodeListAsync(Skip, Take, searchKey, cancellationToken);
        return new()
        {
            TotalRecords = couponCodeList.Total,
            FilterRecords = couponCodeList.Filtered,
            CouponCode = [.. couponCodeList.CouponCode.Select(coupon => new CouponCodeDto()
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
                IsActive = coupon.IsActive,
                Id=coupon.Id
            })]
        };
    }

    /// <summary>
    /// Get coupon code export results
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<CouponCodeExporDto>> ExportAllAsync()
    {
        var couponCodeExportData = await _couponCodeRepository.ExportAllAsync();

        return couponCodeExportData.Select(coupon => new CouponCodeExporDto()
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
            IsActive = coupon.IsActive,
            Id = coupon.Id
        }).AsParallel();
    }

    /// <summary>
    /// Get the single couponcode information
    /// </summary>
    /// <param name="couponCodeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CouponCodeDto?> GetCouponCodeByIdAsync(int couponCodeId, CancellationToken cancellationToken)
    {
        var coupon = await _couponCodeRepository.GetCouponCodeByIdAsync(couponCodeId, cancellationToken);

        if (coupon == null) throw new NullReferenceException();

        return new()
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
            IsActive = coupon.IsActive,
            Id = coupon.Id
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
            .Select(coupon => new CouponCodeDto()
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
                IsActive = coupon.IsActive,
                Id = coupon.Id
            }).AsQueryable();
    }
}

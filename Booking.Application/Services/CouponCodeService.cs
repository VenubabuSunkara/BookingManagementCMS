using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.DomainServices.DataTableLoader;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using System.Runtime.InteropServices;

namespace Booking.Application.Services;

public sealed class CouponCodeService(ICouponCodeRepository couponCodeRepository,
                                       IDataTableService dataTableService) : ICouponCodeService
{
    private readonly ICouponCodeRepository _couponCodeRepository = couponCodeRepository;
    private readonly IDataTableService _dataTableService = dataTableService;
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
            Code = couponCodeDto.Code ?? string.Empty,
            ValidityFrom = couponCodeDto.ValidityFrom,
            ValidityTo = couponCodeDto.ValidityTo,
            PriceRangeMin = couponCodeDto.PriceRangeMin,
            PriceRangeMax = couponCodeDto.PriceRangeMax,
            DiscountType = couponCodeDto.DiscountType ?? string.Empty,
            DiscountValue = couponCodeDto.DiscountValue ?? string.Empty,
            CreatedOn = couponCodeDto.CreatedOn,
            UpdatedOn = couponCodeDto.UpdatedOn,
            CreatedBy = couponCodeDto.CreatedBy,
            UpdatedBy = couponCodeDto.UpdatedBy,
            MediaUrl = couponCodeDto.MediaUrl ?? string.Empty
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
            Id = couponCodeDto.CouponCodeId,
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
    public async Task<bool> FindCouponCodeAsync([Optional] int couponCodeId, [Optional] string couponCode, CancellationToken cancellationToken = default)
    {
        return await _couponCodeRepository.FindCouponCodeAsync(couponCodeId, couponCode, cancellationToken);
    }

    /// <summary>
    /// Get all the couponcodes
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataTableResponseDto<CouponCodeDto>> GetAllCouponCodesAsync(DataTableRequestDto dataTableRequest, string[] searchColumns, CancellationToken cancellationToken)
    {
        var couponCodeQuarable = _couponCodeRepository.GetQuarableCouponCodeData();
        DataTableRequestEntity dataTableRequestEntity = new()
        {
            draw = dataTableRequest.draw,
            start = dataTableRequest.start,
            length = dataTableRequest.length,
            search = new()
            {
                regex = dataTableRequest.search?.regex ?? string.Empty,
                value = dataTableRequest.search?.value ?? string.Empty
            },
            order = [.. dataTableRequest.order.Select(s => new DataTableRequestEntity.Order
            {
                column = s.column,
                dir = s.dir
            }).AsParallel()],
            columns = [.. dataTableRequest.columns.Select(s => new  DataTableRequestEntity.Column()
            {
                data = s.data,
                name = s.name,
                orderable = s.orderable,
                searchable = s.searchable,
                search = new()
                {
                    regex = s.search?.regex ?? string.Empty,
                    value = s.search?.value ?? string.Empty
                }
            }).AsParallel()]
        };
        var couponCodeList = await _dataTableService.GetDataAsync<CouponCodeEntity>(couponCodeQuarable, dataTableRequestEntity, []);

        return new()
        {
            draw = couponCodeList.draw,
            recordsFiltered = couponCodeList.recordsFiltered,
            recordsTotal = couponCodeList.recordsTotal,
            data = [.. couponCodeList.data.Select(s => new CouponCodeDto
            {
                CouponCodeId = s.Id,
                Code = s.Code,
                ValidityFrom = s.ValidityFrom,
                ValidityTo = s.ValidityTo,
                PriceRangeMin = s.PriceRangeMin,
                PriceRangeMax = s.PriceRangeMax,
                DiscountType = s.DiscountType ?? string.Empty,
                DiscountValue = s.DiscountValue ?? string.Empty,
                MediaUrl = s.MediaUrl ?? string.Empty,
                UpdatedOn = s.UpdatedOn
            }).AsParallel()]
        };
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
            CouponCodeId = couponCode.Id,
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
                CouponCodeId = s.Id,
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

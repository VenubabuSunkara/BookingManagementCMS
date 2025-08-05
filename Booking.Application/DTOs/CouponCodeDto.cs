namespace Booking.Application.DTOs;

public class CouponCodeDto
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public DateOnly? ValidityFrom { get; set; }

    public DateOnly? ValidityTo { get; set; }

    public decimal? PriceRangeMin { get; set; }

    public decimal? PriceRangeMax { get; set; }

    public string? DiscountType { get; set; }

    public string? DiscountValue { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public string? MediaUrl { get; set; }
}

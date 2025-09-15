namespace Booking.Infrastructure.Data.Models;
public partial class Payment
{
    public int PaymentId { get; set; }

    public int BookingOrderId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMode { get; set; } = null!;

    public DateTime? PaymentDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual BookingOrder BookingOrder { get; set; } = null!;
}

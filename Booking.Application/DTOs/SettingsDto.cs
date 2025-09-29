namespace Booking.Application.DTOs
{
    public class SettingsDto
    {
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public DateTime? CreatedOn { get; set; }
        public int? Id { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}

namespace Booking.Domain.Entities
{
    public class DriverVehicleTableEntity
    {
        public int Total { get; set; }
        public int Filtered { get; set; }
        public IEnumerable<DriverVehicleEntity> DriverVehicle { get; set; } = [];

    }
}

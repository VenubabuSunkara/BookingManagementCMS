using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class DriverVehicleFullEntity
    {
        public DriverEntity Driver { get; set; } = new DriverEntity();
        public VehicleEntity Vehicle { get; set; } = new VehicleEntity();
        public IEnumerable<VehicleMedia> VehicleMedia { get; set; } = [];
        public IEnumerable<DriverVehicleAvailabilityEntity> DriverVehicleAvailabilityEntities { get; set; } = [];
        public IEnumerable<DriverRatingEntity> DriverRatingEntities { get; set; } = [];
        public IEnumerable<VehicleRatingEntity> VehicleRatingEntities { get; set; } = [];
        public IEnumerable<FeatureEntity> FeatureEntities { get; set; } = [];
        public IEnumerable<BookingOrderEntity> BookingOrdersEntities { get; set; } = [];
        public IEnumerable<PaymentEntity> PaymentEntities { get; set; } = [];
    }
}

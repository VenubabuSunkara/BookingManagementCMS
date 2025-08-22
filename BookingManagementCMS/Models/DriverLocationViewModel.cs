using System.Collections.Generic;

namespace BookingManagementCMS.Models
{
    public class DriverVehicleViewModel
    {
        public int DriverId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DriverDescription { get; set; } = string.Empty;
        public string DriverImage { get; set; } = string.Empty;
        public string DriverFullName
        {
            get { return string.Concat(FirstName, LastName); }
            set { value = string.Concat(FirstName, LastName); }
        }
    }
    public class DriverVehicleListViewModel
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public int VehicleId { get; set; }
        public string VehicleName { get; set; } = string.Empty;
        public string VehicleImage { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
    }
}

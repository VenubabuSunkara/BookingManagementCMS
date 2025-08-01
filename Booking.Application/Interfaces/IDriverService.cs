using Booking.Application.DTOs;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    /*
     * Required Actions 
     * 1. Get All Drivers With Pagination and search  -- Super admin
     * 2. Approve Driver  --- Super admin
     * 3. Update Driver Availability Schedule  -- Super admin and Driver
     * 4. Update Driver Details -- Driver
     * 5. Update Vehicle Details  --driver
     * 6. View Bookings  -- Driver and super admin
     * 7. View Orders -- Driver and super admin
     * 8. View Reviews -- driver and super admin
     * 9. InActive/DeActivate
     * 10. Export  -- Super admin
     * 11. Import Vehicle and Driver -- super admin
     * 12. Bulk delete -- super admin
     * 13. Transfer Schedule to other driver -- super admin
     */
    public interface IDriverService
    {
        Task<IEnumerable<DriverDto>> GetAllAsync();
        Task<IEnumerable<DriverVehicleDto>> GetDriverVehicleList(int pageIndex, int pageSize, string searchKey = "");
        Task<IEnumerable<VehicleMediaDto>> GetVehicleMediaList(int vehicleId);
    }
}

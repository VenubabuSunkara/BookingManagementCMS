using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using Repository;
using Repository.Interfaces;

namespace BookingManagementCMS.Controllers
{
    public class DriverVehicleController : Controller
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
        private readonly IDriverVehicleRepository _driverVehicleRepository;
        private readonly IDataTableRepository _dataTableRepository;
        public DriverVehicleController(IDriverVehicleRepository driverVehicleRepository,
            IDataTableRepository dataTableRepository)
        {
            _driverVehicleRepository = driverVehicleRepository;
            _dataTableRepository = dataTableRepository;
        }

        public async Task<bool> Approve(int DriverVehileId)
        {
            if (DriverVehileId == 0)
                throw new ArgumentException();
            return await _driverVehicleRepository.ApproveAsync(DriverVehileId, true);
        }
        //
        public async Task<IActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        [HttpPost]
        public async Task<IActionResult> LoadDriverData(DatatableRequest request, CancellationToken cancellationToken)
        {
            var driverquery = _driverVehicleRepository.GetAllQuery().Include(c => c.DriverVehicleMedia);
            var result = await _dataTableRepository.GetDataAsync(driverquery, request, []);
            return Json(new
            {
                draw = result.draw,
                recordsFiltered = result.recordsFiltered,
                recordsTotal = result.recordsTotal,
                data = result?.data?.Select(x => new
                {
                    x.VehicleName,
                    x.Capacity,
                    Photo = x.DriverVehicleMedia
                             .FirstOrDefault(m => m.IsDefault)?
                             .MediaValue,
                    VehicleType = x.VehicleType,
                    DriverName = x.DriverFirstName + " " + x.DriverLastName,
                    Contact = x.DriverContact,
                    CreatedDate = x.CreatedOn,
                    x.Id
                }).ToArray()
            });
        }
        public async Task<IActionResult> Create(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index", new DriverAndVehicle());
                }, token);
            return await Task.Run(() =>
              {
                  return View("AddVehicle", new DriverAndVehicle());
              }, token);
        }
    }
}

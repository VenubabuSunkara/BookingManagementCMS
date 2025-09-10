using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Infrastructure.Data.Models;
using Booking.Web.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Common;
using System.Diagnostics;

namespace Booking.Web.Controllers
{    /*
  * Required Actions 
  * 1. Get All Drivers With Pagination and search  -- Super admin   -- Done
  * 2. Approve Driver  --- Super admin   -- Done
  * 3. Update Driver Availability Schedule  -- Super admin and Driver
  * 4. Update Driver Details -- Driver
  * 5. Update Vehicle Details  --driver
  * 6. View Bookings  -- Driver and super admin  -- 
  * 7. View Orders -- Driver and super admin
  * 8. View Reviews -- driver and super admin
  * 9. InActive/DeActivate
  * 10. Export  -- Super admin
  * 11. Import Vehicle and Driver -- super admin
  * 12. Bulk delete -- super admin
  * 13. Transfer Schedule to other driver -- super admin
  */
    [Authorize(Roles = "Admin")]
    public class DriverController : BaseController
    {

        private readonly ILogger<DriverController> _logger;
        private readonly IDriverService _driverService;
        private readonly IBookingService _bookingService;
        private readonly IBookingDetailsService _bookingDetailsService;
        private readonly IVehicleService _vehicleService;
        public DriverController(ILogger<DriverController> logger, IDriverService driverService,
            IBookingService bookingService, IBookingDetailsService bookingDetailsService, IVehicleService vehicleService)
        {
            _logger = logger;
            _driverService = driverService;
            _bookingService = bookingService;
            _bookingDetailsService = bookingDetailsService;
            _vehicleService = vehicleService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadDriverData([FromBody] DataTableAjaxPostModel request, CancellationToken token)
        {
            try
            {
                string search = "";
                if (!String.IsNullOrEmpty(request.search?.value))
                    search = request.search?.value ?? string.Empty;
                var result = await _driverService.GetDriverListAsync(search, request.length, request.start, token);
                return Json(new
                {
                    draw = request.draw == 0 ? 1 : request.draw,
                    recordsFiltered = result.FilterRecords,
                    recordsTotal = result.TotalRecords,
                    data = result.Driverdtos.AsParallel().ToArray()
                });
            }
            catch (Exception ex)
            {
                return Json("Something went wrong {0}", ex);
            }
        }

        public async Task<IActionResult> DriversList(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveDriver(int DriverId, CancellationToken token)
        {
            if (DriverId == 0)
                return Json("Please select Valid Driver");
            return Json(await _driverService.ApproveDriverAsync(DriverId, token));
        }
        [HttpPost]
        public async Task<IActionResult> RejectDriver(int DriverId, CancellationToken token)
        {
            if (DriverId == 0)
                return Json("Please select Valid Driver");
            return Json(await _driverService.RejectDriverAsync(DriverId, token));
        }
        public async Task<IActionResult> Preview(int DriverId, CancellationToken token)
        {
            var driverInfo = await _driverService.GetDriverAsync(DriverId, token);
            return View("Preview", driverInfo);
        }
        [HttpPost]
        public async Task<IActionResult> ProcessDriver(int driverId, string actionType, CancellationToken token)
        {
            if (actionType == "Approve")
            {
                await _driverService.ApproveDriverAsync(driverId, token);
            }
            else if (actionType == "Reject")
            {
                await _driverService.RejectDriverAsync(driverId, token);
            }
            return RedirectToAction("DriversList");
        }
        public async Task<IActionResult> ExportAll(CancellationToken token)
        {
            var data = await _driverService.ExportAllAsync(token); // fetch unpaginated filtered data
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Drivers");
            worksheet.Cell(1, 1).InsertTable(data);
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "DriversList.xlsx");
        }
        [HttpGet]
        public async Task<IActionResult> AssignVehicle(int DriverId, CancellationToken token)
        {
            var vehicles = await _vehicleService.GetUnAssignedVehiclesList(token);
            UnassignedVehiclesModel model = new()
            {
                UnassignedVehicles = [.. vehicles.Select(x => new SelectListItem()
                {
                    Text = x.RegistrationNumber,
                    Value = x.Id.ToString()
                })],
                DriverId = DriverId
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AssignVehicle(UnassignedDriversModel model, CancellationToken token)
        {
            var vehicles = await _vehicleService.GetUnAssignedVehiclesList(token);

            return RedirectToAction("DriversList");
        }
    }
}

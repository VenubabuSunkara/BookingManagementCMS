using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interfaces;

namespace BookingManagementCMS.Controllers
{
    public class DriverVehicleController : Controller
    {
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
        public async Task<IActionResult> Index()
        {
            //await _driverVehicleRepository.GetAllAsync()

            return View();
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
                    CreatedDate = x.CreatedOn
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

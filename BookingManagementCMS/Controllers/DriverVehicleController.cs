using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace BookingManagementCMS.Controllers
{
    public class DriverVehicleController_1 : Controller
    {
        private readonly IDriverVehicleRepository _driverVehicleRepository;
        public DriverVehicleController_1(IDriverVehicleRepository driverVehicleRepository)
        {
            _driverVehicleRepository = driverVehicleRepository;
        }
        //
        public async Task<IActionResult> Index()
        {
            return View(await _driverVehicleRepository.GetAllAsync());
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

using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace BookingManagementCMS.Controllers
{
    public class DriverVehicleController : Controller
    {
        private readonly IDriverVehicleRepository _driverVehicleRepository;
        public DriverVehicleController(IDriverVehicleRepository driverVehicleRepository)
        {
            _driverVehicleRepository = driverVehicleRepository;
        }
        //
        public async Task<IActionResult> Index()
        {
            return View(await _driverVehicleRepository.GetAllAsync());
        }
    }
}

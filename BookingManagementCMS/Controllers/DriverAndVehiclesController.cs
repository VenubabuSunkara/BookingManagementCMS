using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace CMS.Controllers
{
    public class DriverAndVehiclesController : Controller
    {
        private readonly BookingManagementCmsContext _context;

        public DriverAndVehiclesController(BookingManagementCmsContext context)
        {
            _context = context;
        }

        // GET: DriverAndVehicles
        public async Task<IActionResult> Index()
        {
            var bookingManagementCmsContext = _context.DriverAndVehicles.Include(d => d.VehicleType);
            return View(await bookingManagementCmsContext.ToListAsync());
        }

        // GET: DriverAndVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverAndVehicle = await _context.DriverAndVehicles
                .Include(d => d.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driverAndVehicle == null)
            {
                return NotFound();
            }

            return View(driverAndVehicle);
        }

        // GET: DriverAndVehicles/Create
        public IActionResult Create()
        {
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "VehicleType1");
            return View();
        }

        // POST: DriverAndVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DriverFirstName,DriverLastName,DriverContact,DriverEmail,DriverPhoto,DriverLicenceNumber,Description,VehicleName,VehicleNumber,VehicleTypeId,Features,AboutOnVehicle,Capacity,IsAvailable,CreatedBy,UpdatedBy,CreatedOn,UpdatedOn")] DriverAndVehicle driverAndVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driverAndVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "VehicleType1", driverAndVehicle.VehicleTypeId);
            return View(driverAndVehicle);
        }

        // GET: DriverAndVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverAndVehicle = await _context.DriverAndVehicles.FindAsync(id);
            if (driverAndVehicle == null)
            {
                return NotFound();
            }
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "VehicleType1", driverAndVehicle.VehicleTypeId);
            return View(driverAndVehicle);
        }

        // POST: DriverAndVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DriverFirstName,DriverLastName,DriverContact,DriverEmail,DriverPhoto,DriverLicenceNumber,Description,VehicleName,VehicleNumber,VehicleTypeId,Features,AboutOnVehicle,Capacity,IsAvailable,CreatedBy,UpdatedBy,CreatedOn,UpdatedOn")] DriverAndVehicle driverAndVehicle)
        {
            if (id != driverAndVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driverAndVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverAndVehicleExists(driverAndVehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "VehicleType1", driverAndVehicle.VehicleTypeId);
            return View(driverAndVehicle);
        }

        // GET: DriverAndVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverAndVehicle = await _context.DriverAndVehicles
                .Include(d => d.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driverAndVehicle == null)
            {
                return NotFound();
            }

            return View(driverAndVehicle);
        }

        // POST: DriverAndVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driverAndVehicle = await _context.DriverAndVehicles.FindAsync(id);
            if (driverAndVehicle != null)
            {
                _context.DriverAndVehicles.Remove(driverAndVehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverAndVehicleExists(int id)
        {
            return _context.DriverAndVehicles.Any(e => e.Id == id);
        }
    }
}

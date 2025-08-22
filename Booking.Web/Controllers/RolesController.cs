using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class RolesController(IRoleService roleService, ILogger<RolesController> logger) : BaseController
    {
        private readonly IRoleService _roleService = roleService;
        private readonly ILogger<RolesController> _logger = logger;
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return await Task.Run(() => View(), cancellationToken);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadData([FromBody] DataTableAjaxPostModel request, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetAllRoles();
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = roles.Count(),
                recordsTotal = roles.Count(),
                data = roles.Select(x => new
                {
                    x.Name,
                    x.Notes,
                    CreatedOn = x.CreatedOn.HasValue ? x.CreatedOn.Value.ToShortDateString() : "N/A",
                    x.Id,
                    UpdatedOn = x.UpdatedOn.HasValue ? x.UpdatedOn.Value.ToShortDateString() : "N/A",

                }).ToArray()
            });
        }
        /// <summary>
        /// Redirect to create page
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            return await Task.Run(() =>
            {
                return View("Create", new RoleDto());
            }, token);
        }
        /// <summary>
        /// Fetching Data for edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return View("Create", role);
        }
        /// <summary>
        /// AJAX call for role name uniqueness check
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> IsRoleNameAvailable(string name, int id = 0)
        {
            var exists = await _roleService.ExistsByNameAsync(name, id);
            return Json(!exists);
        }
        /// <summary>
        /// Create or Update Role
        /// </summary>
        /// <param name="rolePayload"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(RoleDto role, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            if (!ModelState.IsValid)
                return View("Form", role);
            bool exists = await _roleService.ExistsByNameAsync(role.Name, role.Id);
            if (exists)
            {
                ModelState.AddModelError("Name", "This role name already exists.");
                return View("Form", role);
            }
            if (role.Id > 0)
            {
                var existingRole = await _roleService.GetByIdAsync(role.Id);
                if (existingRole == null)
                    return NotFound();
                existingRole.Name = role.Name;
                existingRole.Notes = role.Notes;

                await _roleService.UpdateAsync(existingRole);
                TempData["SuccessMessage"] = "Role updated successfully!";
            }
            else
            {
                role.CreatedOn = DateTime.UtcNow;
                await _roleService.CreateAsync(role);
                TempData["SuccessMessage"] = "Role created successfully!";
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Deleting Role by RoleId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            await _roleService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Role deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}

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
        public async Task<IActionResult> LoadData([FromBody] DataTableAjaxPostModel request, CancellationToken token)
        {
            var roles = await _roleService.GetAllRoles(token);
            return Json(new
            {
                draw = request.draw == 0 ? 1 : request.draw,
                recordsFiltered = roles.Count(),
                recordsTotal = roles.Count(),
                data = roles.Select(x => new
                {
                    x.Name,
                    x.Id
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
                RoleDto dto = new()
                {
                    isEdit = false
                };
                return View("Create", dto);
            }, token);
        }
        /// <summary>
        /// Fetching Data for edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(string id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            var role = await _roleService.GetByIdAsync(id, token);
            if (role == null) return NotFound();
            role.isEdit = true;
            return View("Create", role);
        }
        /// <summary>
        /// AJAX call for role name uniqueness check
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> IsRoleNameAvailable(string name, CancellationToken token, int id = 0)
        {
            var exists = await _roleService.ExistsByNameAsync(name, token, id);
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
                return View("Create", role);
            //bool exists = await _roleService.ExistsByNameAsync(role.Name, token);
            if (role.isEdit == true)
            {
                await _roleService.UpdateAsync(role, token);
            }
            else
            {
                await _roleService.CreateAsync(role, token);
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
        public async Task<IActionResult> DeleteConfirmed(string id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            await _roleService.DeleteAsync(id, token);
            TempData["SuccessMessage"] = "Role deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}

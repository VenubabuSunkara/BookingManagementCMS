using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using Repository;
using Repository.Interfaces;

namespace CMS.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRolesRepository _rolesRepository;
        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }
        /// <summary>
        /// Fetching All Roles Data
        /// </summary>
        /// <returns></returns>
         [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var totalRecords = await _rolesRepository.GetAllRolesByPaginationAsync();
            var rolesQuery = await _rolesRepository.GetAllAsync();
            var roles = await rolesQuery
                .OrderByDescending(r => r.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            ViewData["CurrentPage"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)totalRecords / pageSize);
            return View(roles);
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
                return View("Create", new Role());
            }, token);
        }
        /// <summary>
        /// Fetching Data for edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            var role = await _rolesRepository.GetByIdAsync(id);
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
            var exists = await _rolesRepository.ExistsByNameAsync(name, id);
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
        public async Task<IActionResult> Save(Role role, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            if (!ModelState.IsValid)
                return View("Form", role);
            bool exists = await _rolesRepository.ExistsByNameAsync(role.Name, role.Id);
            if (exists)
            {
                ModelState.AddModelError("Name", "This role name already exists.");
                return View("Form", role);
            }
            if (role.Id > 0)
            {
                var existingRole = await _rolesRepository.GetByIdAsync(role.Id);
                if (existingRole == null)
                    return NotFound();
                existingRole.Name = role.Name;
                existingRole.Notes = role.Notes;
                existingRole.UpdatedAt = DateTime.UtcNow;

                await _rolesRepository.UpdateAsync(existingRole);
                TempData["SuccessMessage"] = "Role updated successfully!";
            }
            else // Create scenario
            {
                role.CreatedAt = DateTime.UtcNow;
                await _rolesRepository.CreateAsync(role);
                TempData["SuccessMessage"] = "Role created successfully!";
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Fetching details by id for view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            var role = await _rolesRepository.GetByIdAsync(id);
            if (role == null)
                return NotFound();

            return View(role);
        }
       /// <summary>
       /// Deleting Role by RoleId
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.Run(() =>
                {
                    return View("Index");
                }, token);
            await _rolesRepository.DeleteAsync(id);
            TempData["SuccessMessage"] = "Role deleted successfully!";
            return RedirectToAction("Index");
        }

    }
}

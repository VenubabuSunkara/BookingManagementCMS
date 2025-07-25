using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Service;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly RolesService _service;
        public RolesRepository(RolesService rolesServices)
        {
            _service = rolesServices;
        }

        public IQueryable<Role> GetAllAsync()
        {
            return _service.GetAllAsync();
        }
        public async Task<Role?> GetByIdAsync(int id)
        {
            return await (_service.GetByIdAsync(id));
        }
        public async Task<int> GetAllRolesByPaginationAsync()
        {
            return await _service.GetAllRolesByPaginationAsync();
        }
        public async Task<bool> ExistsByNameAsync(string name, int excludeId = 0)
        {
            return await _service.ExistsByNameAsync(name, excludeId);
        }
        public async Task CreateAsync(Role req)
        {
            await _service.CreateAsync(req);
        }
        public async Task UpdateAsync(Role role)
        {
            await _service.UpdateAsync(role);
        }
        public async Task DeleteAsync(int id)
        {
            await (_service.DeleteAsync(id));
        }
    }
}

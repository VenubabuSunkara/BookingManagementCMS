using Entities;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class RolesService : IRolesService
    {
        private readonly BookingManagementCmsContext _context;
        public RolesService(BookingManagementCmsContext context)
        {
            _context = context;
        }
        public IQueryable<Role> GetAllAsync()
        {
            return _context.Roles.AsQueryable().OrderByDescending(r => r.UpdatedAt);
        }
        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }
        public async Task<int> GetAllRolesByPaginationAsync()
        {
            return await _context.Roles.CountAsync();
        }
        public async Task<bool> ExistsByNameAsync(string name, int excludeId = 0)
        {
            return await _context.Set<Role>()
                .AnyAsync(r => r.Name.ToLower() == name.ToLower() && r.Id != excludeId);
        }
        public async Task CreateAsync(Role req)
        {
            if (req != null)
            {
                req.CreatedAt = DateTime.UtcNow;
                req.UpdatedAt = DateTime.UtcNow;
                _context.Roles.Add(req);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Role updatedRole)
        {
            var existing = await _context.Roles.FindAsync(updatedRole.Id);
            if (existing != null)
            {
                existing.Name = updatedRole.Name;
                existing.Notes = updatedRole.Notes;
                existing.UpdatedAt = DateTime.UtcNow;
                //existing.UpdatedBy = updatedRole.UpdatedBy;

                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}

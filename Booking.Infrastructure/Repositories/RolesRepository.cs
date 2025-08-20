using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class RolesRepository(BookingCmsContext context) : IRolesRepository
    {
        private readonly BookingCmsContext _context = context;
        public async Task<int> CreateAsync(RoleEntity req)
        {
            req.CreatedOn = DateTime.UtcNow;
            req.UpdatedOn = DateTime.UtcNow;
            _context.Roles.Add(new Data.Models.Role()
            {
                Name = req.Name,
                Notes = req.Notes,
                UpdatedBy = req.UpdatedBy,
                CreatedOn = req.CreatedOn,
                UpdatedOn = req.UpdatedOn,
                CreatedBy = req.CreatedBy
            });
            return await _context.SaveChangesAsync();
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

        public async Task<bool> ExistsByNameAsync(string name, int excludeId = 0)
        {
            return await _context.Set<Role>()
                   .AnyAsync(r => r.Name.ToLower() == name.ToLower() && r.Id != excludeId);
        }

        public async Task<IEnumerable<RoleEntity>> GetAllRoles()
        {
            return await _context.Roles.Select(role => new RoleEntity()
            {
                Name = role.Name,
                Id = role.Id,
                CreatedBy = role.CreatedBy,
                UpdatedBy = role.UpdatedBy,
                Notes = role.Notes,
                CreatedOn = role.CreatedOn,
                UpdatedOn = role.UpdatedOn
            }).ToListAsync();
        }

        public async Task<RoleEntity?> GetByIdAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return null;
            return new RoleEntity()
            {
                Name = role.Name,
                Id = role.Id,
                CreatedBy = role.CreatedBy,
                UpdatedBy = role.UpdatedBy,
                Notes = role.Notes,
                CreatedOn = role.CreatedOn,
                UpdatedOn = role.UpdatedOn

            };
        }

        public async Task UpdateAsync(RoleEntity role)
        {
            var existing = await _context.Roles.FindAsync(role.Id);
            if (existing != null)
            {
                existing.Name = role.Name;
                existing.Notes = role.Notes;
                existing.UpdatedOn = DateTime.UtcNow;
                existing.UpdatedBy = role.UpdatedBy;
                await _context.SaveChangesAsync();
            }
        }
    }
}

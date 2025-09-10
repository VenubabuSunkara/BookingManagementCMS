using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class RolesRepository(BookingCmsContext context,
        RoleManager<IdentityRole> roleManager) : IRolesRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<int> CreateAsync(RoleEntity req, CancellationToken token)
        {
            IdentityRole role = new()
            {
                Name = req.Name,
            };
            var res = await _roleManager.CreateAsync(role);
            if (res.Succeeded)
                return 1;
            return 0;
        }

        public async Task DeleteAsync(string id, CancellationToken token)
        {
            IdentityRole role = new()
            {
                Id = id
            };
            await _roleManager.DeleteAsync(role);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken token, int excludeId = 0)
        {
            return await _roleManager.Roles.AnyAsync(x => x.Name.Equals(name), token);
        }

        public async Task<IEnumerable<RoleEntity>> GetAllRoles(CancellationToken token)
        {
            var Roles = await _roleManager.Roles.ToListAsync(token);
            return Roles.Select(r => new RoleEntity
            {
                Id = r.Id,
                Name = r.Name ?? string.Empty
            });
        }

        public async Task<RoleEntity?> GetByIdAsync(string id, CancellationToken token)
        {
            var Role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            if (Role == null) return null;
            return new RoleEntity
            {
                Id = Role.Id,
                Name = Role.Name ?? string.Empty
            };
        }

        public async Task UpdateAsync(RoleEntity role, CancellationToken token)
        {
            IdentityRole Identityrole = new()
            {
                Id = role.Id,
                Name = role.Name,
            };
            await _roleManager.UpdateAsync(Identityrole);
        }
    }
}

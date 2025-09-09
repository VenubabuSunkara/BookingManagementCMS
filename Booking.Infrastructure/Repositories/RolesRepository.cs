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
                Id = req.Id,
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
            return await _roleManager.Roles.AnyAsync(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase),
                token);
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
        //public async Task<int> CreateAsync(RoleEntity req)
        //{
        //    _context.Roles.Add(new Data.Models.Role()
        //    {
        //        Name = req.Name
        //    });
        //    return await _context.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var role = await _context.Roles.FindAsync(id);
        //    if (role != null)
        //    {
        //        _context.Roles.Remove(role);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task<bool> ExistsByNameAsync(string name, int excludeId = 0)
        //{
        //    return await _context.Set<Role>()
        //           .AnyAsync(r => r.Name.ToLower() == name.ToLower() && r.Id != excludeId);
        //}

        //public async Task<IEnumerable<RoleEntity>> GetAllRoles()
        //{
        //    return await _context.AspNetRoles
        //        .Select(role => new RoleEntity()
        //        {
        //            Name = role.Name,
        //            Id = role.Id,
        //        }).ToListAsync();
        //}

        //public async Task<RoleEntity?> GetByIdAsync(int id)
        //{
        //    var role = await _context.AspNetRoles.FindAsync(id);
        //    if (role == null) return null;
        //    return new RoleEntity()
        //    {
        //        Name = role.Name,
        //        Id = role.Id

        //    };
        //}

        //public async Task UpdateAsync(RoleEntity role)
        //{
        //    var existing = await _context.AspNetRoles.FindAsync(role.Id);
        //    if (existing != null)
        //    {
        //        existing.Name = role.Name;
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}

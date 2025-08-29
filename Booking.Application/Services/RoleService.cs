using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Booking.Application.Services
{
    public class RoleService(IRolesRepository rolesRepository, UserManager<IdentityUser> userManager) : IRoleService
    {
        private readonly IRolesRepository _rolesRepository = rolesRepository;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        public async Task<int> CreateAsync(RoleDto req)
        {
            return await _rolesRepository.CreateAsync(new Domain.Entities.RoleEntity()
            {
                Name = req.Name
            });
        }

        public async Task DeleteAsync(int id)
        {
            await _rolesRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsByNameAsync(string name, int excludeId = 0)
        {
            return await _rolesRepository.ExistsByNameAsync(name, excludeId);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles()
        {
            var roles = await _rolesRepository.GetAllRoles();
            return roles.Select(x => new RoleDto()
            {
                Name = x.Name,
                Id = x.Id
            });
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var role = await _rolesRepository.GetByIdAsync(id);
            if (role == null) return null;
            return new RoleDto()
            {
                Name = role.Name,
                Id = role.Id
            };
        }

        public async Task UpdateAsync(RoleDto role)
        {
            await _rolesRepository.UpdateAsync(new Domain.Entities.RoleEntity()
            {
                Name = role.Name,
                Id = role.Id
            });
        }
    }
}

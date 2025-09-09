using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Services
{
    public class RoleService(IRolesRepository rolesRepository, UserManager<IdentityUser> userManager) : IRoleService
    {
        private readonly IRolesRepository _rolesRepository = rolesRepository;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        public async Task<int> CreateAsync(RoleDto req, CancellationToken token)
        {
            return await _rolesRepository.CreateAsync(new Domain.Entities.RoleEntity()
            {
                Name = req.Name
            }, token);
        }

        public async Task DeleteAsync(string id, CancellationToken token)
        {
            await _rolesRepository.DeleteAsync(id, token);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken token, int excludeId = 0)
        {
            return await _rolesRepository.ExistsByNameAsync(name, token, excludeId);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles(CancellationToken token)
        {
            var roles = await _rolesRepository.GetAllRoles(token);
            return roles.Select(x => new RoleDto()
            {
                Name = x.Name,
                Id = x.Id
            });
        }

        public async Task<RoleDto?> GetByIdAsync(string id, CancellationToken token)
        {
            var role = await _rolesRepository.GetByIdAsync(id, token);
            if (role == null) return null;
            return new RoleDto()
            {
                Name = role.Name,
                Id = role.Id
            };
        }

        public async Task UpdateAsync(RoleDto role, CancellationToken token)
        {
            await _rolesRepository.UpdateAsync(new Domain.Entities.RoleEntity()
            {
                Name = role.Name,
                Id = role.Id
            }, token);
        }
    }
}

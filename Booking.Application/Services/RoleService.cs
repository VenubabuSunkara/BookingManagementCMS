using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Booking.Application.Services
{
    public class RoleService(IRolesRepository rolesRepository) : IRoleService
    {
        private readonly IRolesRepository _rolesRepository = rolesRepository;
        public async Task<int> CreateAsync(RoleDto req)
        {
            return await _rolesRepository.CreateAsync(new Domain.Entities.RoleEntity()
            {
                Name = req.Name,
                Notes = req.Notes,
                CreatedBy = req.CreatedBy,
                UpdatedBy = req.UpdatedBy,
                CreatedOn = req.CreatedOn,
                UpdatedOn = req.UpdatedOn,
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
                Notes = x.Notes,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy,
                CreatedOn = x.CreatedOn,
                UpdatedOn = x.UpdatedOn,
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
                Notes = role.Notes,
                CreatedBy = role.CreatedBy,
                UpdatedBy = role.UpdatedBy,
                CreatedOn = role.CreatedOn,
                UpdatedOn = role.UpdatedOn,
                Id = role.Id
            };
        }

        public async Task UpdateAsync(RoleDto role)
        {
            await _rolesRepository.UpdateAsync(new Domain.Entities.RoleEntity()
            {
                Name = role.Name,
                Notes = role.Notes,
                UpdatedBy = role.UpdatedBy,
                UpdatedOn = role.UpdatedOn,
                Id = role.Id
            });
        }
    }
}

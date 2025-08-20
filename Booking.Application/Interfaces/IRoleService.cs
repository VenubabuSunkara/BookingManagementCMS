using Booking.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDto?> GetByIdAsync(int id);
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<bool> ExistsByNameAsync(string name, int excludeId = 0);
        Task<int> CreateAsync(RoleDto req);
        Task UpdateAsync(RoleDto role);
        Task DeleteAsync(int id);
    }
}

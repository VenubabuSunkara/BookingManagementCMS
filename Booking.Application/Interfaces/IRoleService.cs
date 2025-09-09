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
        Task<RoleDto?> GetByIdAsync(string id, CancellationToken token);
        Task<IEnumerable<RoleDto>> GetAllRoles(CancellationToken token);
        Task<bool> ExistsByNameAsync(string name, CancellationToken token, int excludeId = 0);
        Task<int> CreateAsync(RoleDto req, CancellationToken token);
        Task UpdateAsync(RoleDto role, CancellationToken token);
        Task DeleteAsync(string id, CancellationToken token);
    }
}

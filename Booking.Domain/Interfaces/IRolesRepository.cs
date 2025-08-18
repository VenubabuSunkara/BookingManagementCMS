using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IRolesRepository
    {
        Task<RoleEntity?> GetByIdAsync(int id);
        Task<IEnumerable<RoleEntity>> GetAllRoles();
        Task<bool> ExistsByNameAsync(string name, int excludeId = 0);
        Task<int> CreateAsync(RoleEntity req);
        Task UpdateAsync(RoleEntity role);
        Task DeleteAsync(int id);
    }
}

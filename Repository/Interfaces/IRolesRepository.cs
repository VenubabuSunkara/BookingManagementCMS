using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRolesRepository
    {
        Task<IQueryable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(int id);
        Task<int> GetAllRolesByPaginationAsync();
        Task<bool> ExistsByNameAsync(string name, int excludeId = 0);
        Task CreateAsync(Role req);
        Task UpdateAsync(Role role);
        Task DeleteAsync(int id);
    }
}

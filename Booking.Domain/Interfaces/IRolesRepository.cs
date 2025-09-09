using Booking.Domain.Entities;
namespace Booking.Domain.Interfaces
{
    public interface IRolesRepository
    {
        Task<RoleEntity?> GetByIdAsync(string id, CancellationToken token);
        Task<IEnumerable<RoleEntity>> GetAllRoles(CancellationToken token);
        Task<bool> ExistsByNameAsync(string name, CancellationToken token, int excludeId = 0);
        Task<int> CreateAsync(RoleEntity req, CancellationToken token);
        Task UpdateAsync(RoleEntity role, CancellationToken token);
        Task DeleteAsync(string id, CancellationToken token);
    }
}

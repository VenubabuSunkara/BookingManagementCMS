using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IPageConfigurationRepository
    {
        Task AddAsync(PageConfigurationEntity pageConfiguration, CancellationToken token);
        Task<PageConfigurationEntity?> GetByIdAsync(int id, CancellationToken token);
        Task<IEnumerable<PageConfigurationEntity>> GetAllAsync(CancellationToken token);
        Task<PageConfigurationTableEntity> GetAllAsync(int skip, int take, string search, CancellationToken token);

        Task UpdateAsync(PageConfigurationEntity pageConfiguration, CancellationToken token);
        Task DeleteAsync(int id, CancellationToken token);
    }
}

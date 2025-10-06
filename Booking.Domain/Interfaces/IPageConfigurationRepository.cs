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
        Task AddAsync(PageConfiguration pageConfiguration, CancellationToken token);
        Task<PageConfiguration?> GetByIdAsync(int id, CancellationToken token);
        Task<IEnumerable<PageConfiguration>> GetAllAsync(CancellationToken token);
        Task UpdateAsync(PageConfiguration pageConfiguration, CancellationToken token);
        Task DeleteAsync(int id, CancellationToken token);
    }
}

using Booking.Application.DTOs.Pages;
using Booking.Domain.Entities;

namespace Booking.Application.Interfaces
{
    public interface IPageConfigurationService
    {
        Task AddAsync(PageConfigurationDto pageConfiguration, CancellationToken token);
        Task<PageConfigurationDto?> GetByIdAsync(int id, CancellationToken token);
        Task<IEnumerable<PageConfigurationDto>> GetAllAsync(CancellationToken token);
        Task<PageConfigurationTableDto> GetAllAsync(int skip, int take, string search, CancellationToken token);
        Task UpdateAsync(PageConfigurationDto pageConfiguration, CancellationToken token);
        Task DeleteAsync(int id, CancellationToken token);
    }
}

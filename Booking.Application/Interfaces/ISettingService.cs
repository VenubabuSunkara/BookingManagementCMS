using Booking.Application.DTOs;

namespace Booking.Application.Interfaces
{
    public interface ISettingService
    {
        Task<IEnumerable<SettingsDto>> GetAllSettings(CancellationToken token);
        Task<int> CreateSetting(SettingsDto setting, CancellationToken token);
        Task DeleteSetting(int Id, CancellationToken token);
        Task UpdateSetting(SettingsDto setting, CancellationToken token);
        Task<SettingsDto?> GetSettingById(int Id, CancellationToken token);
    }
}

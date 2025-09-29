using Booking.Domain.Entities;

namespace Booking.Domain.Interfaces
{
    public interface ISettingRepository
    {
        Task<IEnumerable<SettingEntity>> GetAllSettings(CancellationToken token);
        Task<int> CreateSetting(SettingEntity setting, CancellationToken token);
        Task DeleteSetting(int Id, CancellationToken token);
        Task UpdateSetting(SettingEntity setting, CancellationToken token);
        Task<SettingEntity?> GetSettingById(int Id, CancellationToken token);
    }
}

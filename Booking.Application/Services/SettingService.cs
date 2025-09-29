using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class SettingService(ISettingRepository settingRepository) : ISettingService
    {
        private readonly ISettingRepository _settingRepository = settingRepository;
        public async Task<int> CreateSetting(SettingsDto setting, CancellationToken token)
        {
            return await _settingRepository.CreateSetting(new Domain.Entities.SettingEntity()
            {
                Name = setting.Name,
                Value = setting.Value,
                CreatedBy = setting.CreatedBy ?? string.Empty,
                UpdatedBy = setting.UpdatedBy ?? string.Empty,
                CreatedOn = setting.CreatedOn ?? DateTime.UtcNow,
                UpdatedOn = setting.UpdatedOn ?? DateTime.UtcNow,
            }, token);
        }

        public async Task DeleteSetting(int Id, CancellationToken token)
        {
            await _settingRepository.DeleteSetting(Id, token);
        }

        public async Task<IEnumerable<SettingsDto>> GetAllSettings(CancellationToken token)
        {
            var settings = await _settingRepository.GetAllSettings(token);
            return settings.Select(x => new SettingsDto()
            {
                Name = x.Name,
                Value = x.Value,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy,
                CreatedOn = x.CreatedOn,
                UpdatedOn = x.UpdatedOn,
                Id = x.Id
            }).AsParallel();
        }

        public async Task<SettingsDto?> GetSettingById(int Id, CancellationToken token)
        {
            var setting = await _settingRepository.GetSettingById(Id, token);
            if (setting is null) return null;
            return new SettingsDto()
            {
                Name = setting.Name,
                Value = setting.Value,
                CreatedBy = setting.CreatedBy,
                UpdatedBy = setting.UpdatedBy,
                UpdatedOn = setting.UpdatedOn,
                Id = setting.Id,
                CreatedOn = setting.CreatedOn,

            };
        }

        public async Task UpdateSetting(SettingsDto setting, CancellationToken token)
        {
            await _settingRepository.UpdateSetting(new SettingEntity()
            {
                Name = setting.Name,
                Value = setting.Value,
                UpdatedBy = setting.UpdatedBy ?? string.Empty,
                UpdatedOn = setting.UpdatedOn ?? DateTime.UtcNow,
                Id = setting.Id ?? 0,
            }, token);
        }
    }
}

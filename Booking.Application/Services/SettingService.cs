using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class SettingService(ISettingRepository settingRepository) : ISettingService
    {
        private readonly ISettingRepository _settingRepository = settingRepository;
        public async Task<int> CreateSetting(SettingsDto setting)
        {
            return await _settingRepository.CreateSetting(new Domain.Entities.SettingEntity()
            {
                Name = setting.Name,
                Value = setting.Value,
                CreatedBy = setting.CreatedBy,
                UpdatedBy = setting.UpdatedBy,
                CreatedOn = setting.CreatedOn,
                UpdatedOn = setting.UpdatedOn,
            });
        }

        public async Task DeleteSetting(int Id)
        {
            await _settingRepository.DeleteSetting(Id);
        }

        public async Task<IEnumerable<SettingsDto>> GetAllSettings()
        {
            var settings = await _settingRepository.GetAllSettings();
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

        public async Task<SettingsDto> GetSettingById(int Id)
        {
            var setting = await _settingRepository.GetSettingById(Id);
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

        public async Task UpdateSetting(SettingsDto setting)
        {
            await _settingRepository.UpdateSetting(new Domain.Entities.SettingEntity()
            {
                Name = setting.Name,
                Value = setting.Value,
                UpdatedBy = setting.UpdatedBy,
                UpdatedOn = setting.UpdatedOn
            });
        }
    }
}

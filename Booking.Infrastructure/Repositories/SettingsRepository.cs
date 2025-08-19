using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using System.Data.Entity;

namespace Booking.Infrastructure.Repositories
{
    public class SettingsRepository(BookingCmsContext context) : ISettingRepository
    {
        private readonly BookingCmsContext _context = context;
        public async Task<int> CreateSetting(SettingEntity setting)
        {
            _context.Configurations.Add(new Data.Models.Configuration()
            {
                KeyName = setting.Name,
                KeyValue = setting.Value,
                CreatedBy = setting.CreatedBy,
                CreatedOn = setting.CreatedOn,
                UpdatedBy = setting.UpdatedBy,
                UpdatedOn = setting.UpdatedOn,
            });
            return await _context.SaveChangesAsync();
        }

        public async Task DeleteSetting(int Id)
        {
            var setting = await _context.Configurations.FindAsync(Id);
            if (setting != null)
            {
                _context.Configurations.Remove(setting);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SettingEntity>> GetAllSettings()
        {
            return await _context.Configurations.Select(x => new SettingEntity()
            {
                Name = x.KeyName,
                Value = x.KeyValue,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                UpdatedBy = x.UpdatedBy,
                UpdatedOn = x.UpdatedOn,
                Id = x.Id
            }).ToListAsync();

        }

        public async Task<SettingEntity> GetSettingById(int Id)
        {
            var existing = await _context.Configurations.FindAsync(Id);
            if (existing == null) return null;
            return new SettingEntity()
            {
                CreatedBy = existing.CreatedBy,
                CreatedOn = existing.CreatedOn,
                UpdatedBy = existing.UpdatedBy,
                Id = existing.Id,
                Value = existing.KeyValue,
                Name = existing.KeyName,
            };
        }

        public async Task UpdateSetting(SettingEntity setting)
        {
            var existing = await _context.Configurations.FindAsync(setting.Id);
            if (existing != null)
            {
                existing.KeyName = setting.Name;
                existing.KeyValue = setting.Value;
                existing.UpdatedOn = setting.UpdatedOn;
                existing.UpdatedBy = setting.UpdatedBy;
                await _context.SaveChangesAsync();
            }
        }
    }
}

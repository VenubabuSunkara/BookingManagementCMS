using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class SettingsRepository(BookingCmsContext context) : ISettingRepository
    {
        private readonly BookingCmsContext _context = context;
        public async Task<int> CreateSetting(SettingEntity setting, CancellationToken token)
        {
            await _context.Configurations.AddAsync(
                new Data.Models.Configuration()
                {
                    KeyName = setting.Name,
                    KeyValue = setting.Value,
                    CreatedBy = setting.CreatedBy,
                    CreatedOn = setting.CreatedOn,
                    UpdatedBy = setting.UpdatedBy,
                    UpdatedOn = setting.UpdatedOn,
                }, token);
            return await _context.SaveChangesAsync(token);
        }

        public async Task DeleteSetting(int Id, CancellationToken token)
        {
            await _context.Configurations.Where(x => x.Id.Equals(Id)).ExecuteDeleteAsync(token);
        }

        public async Task<IEnumerable<SettingEntity>> GetAllSettings(CancellationToken token)
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

        public async Task<SettingEntity?> GetSettingById(int Id, CancellationToken token)
        {
            var existing = await _context.Configurations.FindAsync([Id], cancellationToken: token);
            if (existing == null) return null;
            return new SettingEntity()
            {
                CreatedBy = existing.CreatedBy,
                CreatedOn = existing.CreatedOn,
                Id = existing.Id,
                Value = existing.KeyValue,
                Name = existing.KeyName,
            };
        }

        public async Task UpdateSetting(SettingEntity setting, CancellationToken token)
        {
            await _context.Configurations
                            .Where(x => x.Id.Equals(setting.Id))
                            .ExecuteUpdateAsync(c => c
                                .SetProperty(s => s.KeyName, setting.Name)
                                .SetProperty(s => s.KeyValue, setting.Value)
                                .SetProperty(s => s.UpdatedOn, setting.UpdatedOn)
                                .SetProperty(s => s.UpdatedBy, setting.UpdatedBy),
                                 cancellationToken: token);
        }
    }
}

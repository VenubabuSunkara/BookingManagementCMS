using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface ISettingRepository
    {
        Task<IEnumerable<SettingEntity>> GetAllSettings();
        Task<int> CreateSetting(SettingEntity setting);
        Task DeleteSetting(int Id);
        Task UpdateSetting(SettingEntity setting);
        Task<SettingEntity> GetSettingById(int Id);
    }
}

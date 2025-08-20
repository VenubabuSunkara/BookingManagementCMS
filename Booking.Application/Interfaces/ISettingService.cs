using Booking.Application.DTOs;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface ISettingService
    {
        Task<IEnumerable<SettingsDto>> GetAllSettings();
        Task<int> CreateSetting(SettingsDto setting);
        Task DeleteSetting(int Id);
        Task UpdateSetting(SettingsDto setting);
        Task<SettingsDto> GetSettingById(int Id);
    }
}

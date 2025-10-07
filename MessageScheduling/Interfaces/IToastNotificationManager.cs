using MessageScheduling.Configurations;
using MessageScheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Interfaces
{
    public interface IToastNotificationManager
    {
        Task ShowAsync(string title, string message, ToastType type = ToastType.Information);
        Task ShowAsync(ToastNotificationOptions options);
        void Hide(string tag);
        void HideAll();
    }

}

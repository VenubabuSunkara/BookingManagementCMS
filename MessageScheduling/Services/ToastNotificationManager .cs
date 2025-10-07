using MessageScheduling.Interfaces;
using MessageScheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Services
{
    public class ToastNotificationManager : IToastNotificationManager
    {
        public void Hide(string tag)
        {
            throw new NotImplementedException();
        }

        public void HideAll()
        {
            throw new NotImplementedException();
        }

        public Task ShowAsync(string title, string message, ToastType type = ToastType.Information)
        {
            throw new NotImplementedException();
        }

        public Task ShowAsync(ToastNotificationOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

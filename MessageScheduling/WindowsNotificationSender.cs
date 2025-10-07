using MessageScheduling.Interface;
using MessageScheduling.Interfaces;
using MessageScheduling.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling
{
    public class WindowsNotificationSender : INotificationSender
    {
        private readonly ILogger<WindowsNotificationSender> _logger;
        private readonly IToastNotificationManager _toastNotificationManager;

        public WindowsNotificationSender(
            ILogger<WindowsNotificationSender> logger,
            IToastNotificationManager toastNotificationManager)
        {
            _logger = logger;
            _toastNotificationManager = toastNotificationManager;
        }

        public async Task<NotificationResult> SendAsync(INotification notification)
        {
            try
            {
                var windowsNotification = (WindowsNotification)notification;
                //var toast = new ToastContentBuilder()
                //    .AddText(windowsNotification.Subject)
                //    .AddText(windowsNotification.Body)
                //    .GetToastContent();

                //await _toastNotificationManager.ShowAsync(toast);
                return new NotificationResult(true, notification.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send Windows notification");
                return new NotificationResult(false, notification.Id, ex.Message);
            }
        }
    }
}

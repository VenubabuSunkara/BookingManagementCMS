using MessageScheduling.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Models
{
    public class ToastNotificationOptions
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Tag { get; set; }
        public TimeSpan? Duration { get; set; }
        public ToastType Type { get; set; } = ToastType.Information;
        public string ImagePath { get; set; }
        public Action OnActivated { get; set; }
        public Action OnDismissed { get; set; }
    }
}

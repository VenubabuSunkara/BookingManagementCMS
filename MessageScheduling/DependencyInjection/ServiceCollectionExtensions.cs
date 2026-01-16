using MessageScheduling.Interface;
using MessageScheduling.Interfaces;
using MessageScheduling.Models;
using MessageScheduling.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageScheduling.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNotificationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            //services.Configure<SmtpSettings>(
            //    configuration.GetSection("SmtpSettings"));

            services.AddSingleton<INotificationFactory, NotificationFactory>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<EmailNotificationSender>();
            services.AddScoped<SmsNotificationSender>();
            services.AddScoped<PushNotificationSender>();
            services.AddScoped<WindowsNotificationSender>();

            // Add additional required services
            services.AddSingleton<ITwilioClient, TwilioClient>();
            services.AddSingleton<IToastNotificationManager, ToastNotificationManager>();

            return services;
        }
    }
}

using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateBinder.Interface;
using TemplateBinder.Services;

namespace TemplateBinder
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Register your services here
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestHeadersTotalSize = 1024 * 1024; // e.g., 1 MB
            });
            services.AddSingleton<ITemplateParse, TemplateParse>();
            return services;
        }
    }
}

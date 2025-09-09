using Booking.Application.Services;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestHeadersTotalSize = 1024 * 1024; // e.g., 1 MB
            });
            services.AddDbContext<BookingCmsContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddMemoryCache();
            services.AddSingleton<ITicketStore, MemoryCacheTicketStore>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<ICouponCodeRepository, CouponCodeRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingDetailsRepository, BookingDetailsRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IPackageCategoryRepository, PackageCategoryRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<ISettingRepository, SettingsRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();

            return services;
        }

    }
}

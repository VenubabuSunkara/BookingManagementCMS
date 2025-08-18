using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Repositories;
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
            services.AddDbContext<BookingCmsContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddMemoryCache();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<ICouponCodeRepository, CouponCodeRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingDetailsRepository, BookingDetailsRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();

            return services;
        }

    }
}

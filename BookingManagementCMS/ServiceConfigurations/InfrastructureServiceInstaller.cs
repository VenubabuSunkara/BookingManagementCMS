using CMS.ErrorHandler;
using Data;
using Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interfaces;
using Service.Interfaces;
using Utilities;
using Utilities.Interfaces;

namespace CMS.ServiceConfigurations
{
    public class InfrastructureServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            //DB Context
            services.AddDbContext<BookingManagementCmsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // ====== Add services ======
            var mvcBuilder = services.AddControllersWithViews();

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
            if (!string.IsNullOrEmpty(env) && env.Equals(Environments.Development, StringComparison.OrdinalIgnoreCase))
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }

            // In-memory cache (fast but local to server)
            services.AddMemoryCache();

            // Distributed cache (example using in-memory, swap with Redis/SQL Server if needed)
            services.AddDistributedMemoryCache(); // Or AddStackExchangeRedisCache()

            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "localhost:6379"; // Adjust to your Redis server
            //});

            //services.AddExceptionHandler<GlobalExceptionHandler>();
            //services.AddProblemDetails(options => options.CustomizeProblemDetails = ctx => ctx.ProblemDetails.Extensions.Add("nodeId", Environment.MachineName));
            services.AddHttpContextAccessor();

            //File uploader size limit
            services.Configure<FormOptions>(opts =>
            {
                opts.MultipartBodyLengthLimit = long.MaxValue;
            });

            #region RegisterUtilities
            services.AddScoped<IRemoteHostIpAddress, RemoteHostIpAddress>();
            services.AddScoped<ILogError, LogError>();
            #endregion
        }

    }
}

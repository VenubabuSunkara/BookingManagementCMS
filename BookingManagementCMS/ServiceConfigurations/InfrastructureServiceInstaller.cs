using CMS.ErrorHandler;
using Entities;
using Microsoft.EntityFrameworkCore;
using Utilities;
using Utilities.Interfaces;

namespace CMS.ServiceConfigurations
{
    public class InfrastructureServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var mvcBuilder = services.AddControllersWithViews();

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
            if (!string.IsNullOrEmpty(env) && env.Equals(Environments.Development,StringComparison.OrdinalIgnoreCase))
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }

            //DB Context
            services.AddDbContext<BookingManagementCmsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails(options => options.CustomizeProblemDetails = ctx => ctx.ProblemDetails.Extensions.Add("nodeId", Environment.MachineName));
            services.AddHttpContextAccessor();

            #region RegisterUtilities
            services.AddScoped<IRemoteHostIpAddress, RemoteHostIpAddress>();
            services.AddScoped<ILogError, LogError>();
            #endregion
        }
    }
}

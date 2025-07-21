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
            services.AddControllersWithViews();

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

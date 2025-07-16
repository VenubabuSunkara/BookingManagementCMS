
using Data.Interfaces.IpAddress;
using Data.Interfaces.Logger;
using Data.Repository.IpAddress;
using Data.Repository.Logger;

namespace CMS.ServiceConfigurations
{
    public class DataAccessLayerServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRemoteHostIpAddress, RemoteHostIpAddress>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();
        }
    }
}

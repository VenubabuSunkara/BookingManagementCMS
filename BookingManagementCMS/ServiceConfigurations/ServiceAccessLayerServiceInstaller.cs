
using Repository;
using Repository.Interfaces;
using Service;
using Service.Interfaces;

namespace CMS.ServiceConfigurations;

public class ServiceAccessLayerServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IPromotionServive, PromotionServive>();
        services.AddTransient<IDataTableRepository, DataTableRepository>();
    }
}

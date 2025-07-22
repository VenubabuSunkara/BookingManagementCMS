using Repository;
using Repository.Interfaces;

namespace CMS.ServiceConfigurations;

public class RepositoryLayerServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IPramotionRepository, PramotionRepository>();
    }
}

using CMS.ServiceConfigurations;
using System.Reflection;

namespace CMS.Extensions;

public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection InstallServices(
         this IServiceCollection services, IConfiguration config, params Assembly[] assemblies)
    {

        assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(t => typeof(IServiceInstaller).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>()
            .ToList()
            .ForEach(a =>
            {
                a.Install(services, config);
            });
        return services;
    }
}

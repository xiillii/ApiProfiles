using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApiPersonProfiles.Core.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(assembly);
        services.AddMediatR(configuration =>
                    configuration.RegisterServicesFromAssemblies(assembly));

        return services;    
    }
}

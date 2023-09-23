using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Infrastructure.Persistence.DatabaseContext;
using ApiPersonProfiles.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiPersonProfiles.Infrastructure.Persistence;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services
        , IConfiguration configuration)
    {
        services.AddDbContext<EFDatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ProfilesConnectionString"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoryImpl<>));

        return services;
    }
}

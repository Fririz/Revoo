using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Infrastracture.Persistence;
using UserService.Application.Contracts;
using UserService.Infrastracture.Repositories;
using UserService.Infrastracture.Serialization;


namespace UserService.Infrastracture;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("UserConnectionString")));
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddSingleton<ISerializer, JsonSerializerWrapper>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.Decorate<IUserRepository, CachedUserRepository>();

        return services;
    }
}
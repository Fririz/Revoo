using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Infrastracture.Persistence;
using UserService.Application.Contracts;
using UserService.Infrastracture.Repositories;
using UserService.Infrastracture.Serialization;
using StackExchange.Redis;

namespace UserService.Infrastracture;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("UserConnectionString")));
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");

        });
        services.AddSingleton<ISerializer, JsonSerializerWrapper>();
        
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.Decorate(typeof(IRepositoryBase<>), typeof(CachedRepositoryBase<>));

        services.AddScoped<IUserRepository, UserRepository>();
        //instead of UserRepository we give CachedUserRepository
        services.Decorate<IUserRepository, CachedUserRepository>();

        return services;
    }
}
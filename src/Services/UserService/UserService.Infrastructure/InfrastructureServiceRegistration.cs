using Microsoft.Extensions.DependencyInjection;
namespace UserService.Infrastracture;

public class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
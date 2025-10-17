using UserService.Infrastracture;
using Logging;
using Serilog;
namespace UserService.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Host.UseSerilog((context, config) =>
        {
            SeriLogger.Configure(context, config);
        });
        
        // Add services to the container.
        builder.Services.AddAuthorization();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();

        
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();   
        }
        app.UseAuthorization();
        app.MapControllers();
        app.UseSerilogRequestLogging();
        Log.Information("Starting User Service");
        app.Run();
    }
}
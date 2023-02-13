using CachingExample.ApplicationCore;
using CachingExample.ApplicationCore.Services;

namespace CachingExample.Web;

/// <summary>
/// Extension methods for dependency injection
/// </summary>
public static class StartupExtensions
{
    public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
        services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        services.AddScoped<ICachingService, CachingService>();
        services.AddDistributedMemoryCache();

        services.AddOptions<CacheOptions>()
            .Bind(configuration.GetSection(nameof(CacheOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
using CachingExample.ApplicationCore.Models;

namespace CachingExample.ApplicationCore.Services;

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecast>> GetForecasts();
}

/// <summary>
/// Middle-man service to perform explicit backend caching
/// </summary>
public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _repository;
    private readonly ICachingService<IEnumerable<WeatherForecast>> _cachingService;

    private const string CacheKey = "cacheKey";

    public WeatherForecastService(IWeatherForecastRepository repository,
        ICachingService<IEnumerable<WeatherForecast>> cachingService)
    {
        _repository = repository;
        _cachingService = cachingService;
    }

    public async Task<IEnumerable<WeatherForecast>> GetForecasts()
    {
        // Check if something is in the cache
        var cachedResult = await _cachingService.GetObject(CacheKey);

        // If this is not null, it means we got a cache hit
        if (cachedResult is not null)
        {
            return cachedResult;
        }

        // Get a fresh result
        var result = await _repository.GetForecasts();

        // Set it to the cache
        await _cachingService.SetObject(CacheKey, result);

        // Return the result
        return result;
    }
}
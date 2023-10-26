using CachingExample.ApplicationCore.Models;

namespace CachingExample.ApplicationCore.Services;

/// <summary>
/// Returns weather data
/// </summary>
public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecast>> GetForecasts();
}

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private const int Delay = 5000;
    private const int NumRecords = 1000;

    /// <summary>
    /// Simulates a long-running database query or API call
    /// </summary>
    public async Task<IEnumerable<WeatherForecast>> GetForecasts()
    {
        var random = new Random();

        await Task.Delay(Delay);

        return Enumerable.Range(0, NumRecords)
            .Select(i =>
            {
                var temperature = random.Next(30, 90);
                var dateTime = DateTime.Now.Date - TimeSpan.FromDays(i);

                return new WeatherForecast
                {
                    Temperature = temperature,
                    DateTime = dateTime,
                    Summary = $"Weather summary for item {i}"
                };
            });
    }
}
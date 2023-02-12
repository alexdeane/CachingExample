namespace CachingExample.ApplicationCore.Models;

/// <summary>
/// entity returned from the repository
/// </summary>
public class WeatherForecast
{
    public int Temperature { get; init; }
    public DateTime DateTime { get; init; }
    public string? Summary { get; init; }
}
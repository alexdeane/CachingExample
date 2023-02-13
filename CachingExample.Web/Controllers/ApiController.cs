using CachingExample.ApplicationCore.Models;
using CachingExample.ApplicationCore.Services;
using CachingExample.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CachingExample.Web.Controllers;

/// <summary>
/// Controller with JSON API
/// </summary>
public class ApiController : ControllerBase
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public ApiController(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    /// <summary>
    /// [GET] /api/forecasts/
    /// </summary>
    [TypeFilter(typeof(RequestDurationFilter))]
    [ResponseCache(Duration = 20, NoStore = false)] // This single line does almost everything which the CachingService does
    public Task<IEnumerable<WeatherForecast>> Forecasts()
        => _weatherForecastRepository.GetForecasts();
}
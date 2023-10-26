using CachingExample.ApplicationCore.Models;
using CachingExample.ApplicationCore.Services;
using CachingExample.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

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
    [OutputCache(PolicyName = nameof(Forecasts))] // This single line does almost everything which the CachingService does
    public Task<IEnumerable<WeatherForecast>> Forecasts()
        => _weatherForecastRepository.GetForecasts();
}
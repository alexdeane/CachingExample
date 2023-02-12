using System.Diagnostics;
using CachingExample.ApplicationCore;
using CachingExample.ApplicationCore.Models;
using CachingExample.ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using CachingExample.Web.Models;

namespace CachingExample.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWeatherForecastService _weatherForecastService;

    public HomeController(ILogger<HomeController> logger,
        IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var sw = new Stopwatch();

        sw.Start();
        var results = await _weatherForecastService.GetForecasts();
        sw.Stop();

        var response = new WeatherForecastResponse
        {
            Results = results,
            DurationMs = sw.ElapsedMilliseconds
        };

        return View(response);
    }

    [HttpGet("ResponseCaching")]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index_ResponseCaching(
        [FromServices] IWeatherForecastRepository weatherForecastRepository)
    {
        var sw = new Stopwatch();
        sw.Start();
        // Sidestep the other caching by invoking the repository directly
        var results = await weatherForecastRepository.GetForecasts();
        sw.Stop();

        var response = new WeatherForecastResponse
        {
            Results = results,
            DurationMs = sw.ElapsedMilliseconds
        };

        return View("Index", response);
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public class WeatherForecastResponse
    {
        public IEnumerable<WeatherForecast>? Results { get; init; }
        public long DurationMs { get; init; }
    }
}
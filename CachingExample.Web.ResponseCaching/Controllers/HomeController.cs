using System.Diagnostics;
using CachingExample.ApplicationCore.Services;
using CachingExample.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using CachingExample.Web.Models;

namespace CachingExample.Web.Controllers;

public class HomeController : Controller
{
    private readonly IWeatherForecastService _weatherForecastService;

    public HomeController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    [TypeFilter(typeof(RequestDurationFilter))]
    public async Task<IActionResult> Index()
    {
        var results = await _weatherForecastService.GetForecasts();
        return View(results);
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
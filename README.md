## Overview

Example application with two different kinds of caching on two different endpoints:

* The [HomeController](./CachingExample.Web/Controllers/HomeController.cs) returns a razor page containing preloaded
  weather data. The weather data itself is cached through
  the [WeatherForecastService](./CachingExample.ApplicationCore/Services/WeatherForecastService.cs).
    * The `WeatherForecastService` uses
      the [CachingService](./CachingExample.ApplicationCore/Services/CachingService.cs) (a wrapper
      for `IDistributedCache`) to cache results from the "repository"

* The [ApiController](./CachingExample.Web/Controllers/ApiController.cs) returns JSON formatted weather data. It
  uses [ASP.Net Core Output Caching](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/output?view=aspnetcore-7.0) and does not use the aforementioned `CachingService` at all.
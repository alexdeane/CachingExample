using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CachingExample.Web.Filters;

/// <summary>
/// Filter which measures the duration
/// of a request and sets it to a header value
/// in the response
/// </summary>
public class RequestDurationFilter : IAsyncActionFilter
{
    public const string RequestDurationHeader = "X-Request-Duration";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        await next();

        stopWatch.Stop();

        context.HttpContext.Response.Headers[RequestDurationHeader] = stopWatch.ElapsedMilliseconds.ToString();
    }
}
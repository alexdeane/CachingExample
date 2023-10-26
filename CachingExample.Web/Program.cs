using CachingExample.ApplicationCore.Services;
using CachingExample.Web;
using CachingExample.Web.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationDependencies(builder.Configuration);

// Adds output caching middleware
builder.Services.AddOutputCache(options =>
{
    // Adds a caching policy for use by the Forecasts action
    options.AddPolicy(nameof(ApiController.Forecasts), b =>
    {
        var cacheOptions = builder.Configuration.GetSection(nameof(CacheOptions))
            .Get<CacheOptions>();

        b.Cache().Expire(cacheOptions.ExpiryTime);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Adds the output caching middleware to the request pipeline
app.UseOutputCache();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
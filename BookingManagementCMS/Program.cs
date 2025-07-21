using CMS.Extensions;
using CMS.ServiceConfigurations;
using Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookingManagementCmsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ====== Configure log4net ======
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net("log4net.config"); // Ensure log4net.config exists

// ====== Add services ======
builder.Services.AddControllersWithViews();

// In-memory cache (fast but local to server)
builder.Services.AddMemoryCache();

// Distributed cache (example using in-memory, swap with Redis/SQL Server if needed)
builder.Services.AddDistributedMemoryCache(); // Or AddStackExchangeRedisCache()

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = "localhost:6379"; // Adjust to your Redis server
//});
//Add services to the container.
builder.Services
       .InstallServices(builder.Configuration, typeof(IServiceInstaller).Assembly);

var app = builder.Build();

// ====== Custom Global Exception Handling ======
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        logger.LogError(exceptionHandlerPathFeature?.Error, "Unhandled Exception");

        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/html";

        await context.Response.WriteAsync("<h1>Something went wrong</h1><p>Please try again later.</p>");
    });
});

// ====== Environment-specific settings ======
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// ====== MVC Route ======
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

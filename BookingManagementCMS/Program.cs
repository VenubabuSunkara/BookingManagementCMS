using CMS.Extensions;
using CMS.ServiceConfigurations;
using Data;
using Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Interfaces;
using Service.Interfaces;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services
       .InstallServices(builder.Configuration, typeof(IServiceInstaller).Assembly);

// ====== Configure log4net ======
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net("log4net.config"); // Ensure log4net.config exists

#region Register Services and Repos
//builder.Services.AddScoped<IDriverAndVehicleService, DriverAndVehicleService>();
// or if there’s an interface:
builder.Services.AddScoped<DriverAndVehicleService>();
builder.Services.AddScoped<IDriverVehicleRepository, DriverVehicleRepository>();
#endregion

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
app.MapControllers();
// ====== MVC Route ======
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

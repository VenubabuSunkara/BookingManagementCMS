
using Booking.Application.DTOs;
using Booking.Domain.Entities;
using Booking.Infrastructure.Identity.Data;
using Booking.Web.Controllers;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
namespace Booking.Web.ViewComponents
{
    public class VehicleFeaturesViewComponent(ILogger<VehicleFeaturesViewComponent> logger) : ViewComponent
    {
        private readonly ILogger<VehicleFeaturesViewComponent> _logger = logger;

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }

    }
}

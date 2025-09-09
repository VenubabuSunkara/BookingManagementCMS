
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
    public class VehicleFeaturesViewComponent : ViewComponent
    {
        private readonly ILogger<VehicleFeaturesViewComponent> _logger;
        public VehicleFeaturesViewComponent(ILogger<VehicleFeaturesViewComponent> logger)
        {
            _logger = logger;
        }
        public async Task<IViewComponentResult> InvokeAsync(int VehicleId)
        {
            //var drivers = await Task.FromResult(
            //    _context.Drivers
            //        .Where(d => !isActiveOnly || d.IsActive)
            //        .ToList()
            //);

            return View();
        }

    }
}

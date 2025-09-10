using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace Booking.Web.Controllers
{
    public class MediaController(ILogger<MediaController> logger) : BaseController
    {
        private readonly ILogger<MediaController> _logger = logger;
        public async Task<IActionResult> Index(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return View();
            }, token);
        }
        //[HttpPost]
        //public Task<IActionResult> LoadMedia([FromBody] DataTableAjaxPostModel request, CancellationToken token)
        //{
        //    try
        //    {
        //        string search = "";
        //        if (!String.IsNullOrEmpty(request.search?.value))
        //            search = request.search?.value ?? string.Empty;
        //        var result = await _driverService.GetDriverListAsync(search, request.length, request.start, token);
        //        return Json(new
        //        {
        //            draw = request.draw == 0 ? 1 : request.draw,
        //            recordsFiltered = result.FilterRecords,
        //            recordsTotal = result.TotalRecords,
        //            data = result.Driverdtos.AsParallel().ToArray()
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("Something went wrong {0}", ex);
        //    }
        //}
    }
}

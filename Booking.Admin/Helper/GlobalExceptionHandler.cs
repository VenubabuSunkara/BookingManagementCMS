using Booking.Domain.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SendGrid.Helpers.Errors.Model;
using System.Diagnostics;

namespace Booking.Web.Helper
{

    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;
        public void OnException(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = "Error" };

            switch (context.Exception)
            {
                case DomainException domainException:
                    result.ViewData["Message"] = domainException.Message;
                    context.HttpContext.Response.StatusCode = 400;
                    break;

                case ApplicationException appException:
                    result.ViewData["Message"] = appException.Message;
                    context.HttpContext.Response.StatusCode = 400;
                    break;

                default:
                    HandleExceptionAsync(context.HttpContext, context.Exception).RunSynchronously();
                    result.ViewData["Message"] = "An unexpected error occurred.";
                    context.HttpContext.Response.StatusCode = 500;
                    break;
            }

            context.Result = result;
            context.ExceptionHandled = true;
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = StatusCodes.Status500InternalServerError; // Internal Server Error by default
            switch (exception)
            {
                case ArgumentNullException:
                case ArgumentException:
                    code = StatusCodes.Status400BadRequest;
                    break;
                case KeyNotFoundException:
                    code = StatusCodes.Status404NotFound;
                    break;
                case UnauthorizedAccessException:
                    code = StatusCodes.Status401Unauthorized;
                    break;
                case InvalidOperationException:
                    code = StatusCodes.Status409Conflict;
                    break;
                case BadRequestException:
                    code = StatusCodes.Status400BadRequest;
                    break;
                    // Add more specific exceptions as needed
            }
            var result = System.Text.Json.JsonSerializer.Serialize(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            // Log the exception details
            var trace = new StackTrace(exception, true);
            var frame = trace.GetFrame(0);
            var lineNumber = frame?.GetFileLineNumber();
            var fileName = frame?.GetFileName();
            _logger.LogError(exception, "Exception caught in global handler. File: {FileName}, Line: {LineNumber}, Message: {Message}", fileName, lineNumber, exception.Message);
            await context.Response.WriteAsync(result);
        }
    }
}

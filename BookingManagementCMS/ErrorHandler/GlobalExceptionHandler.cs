using CMS.Extensions;
using Data;
using Data.Interfaces.Logger;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ExceptionServices;

namespace CMS.ErrorHandler;

public class GlobalExceptionHandler(IServiceScopeFactory _scopeFactory,
                                    IWebHostEnvironment _env) : IExceptionHandler
{
    /// <summary>
    ///  Log error
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="exception"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        //Get the class and method name by using fall mechanism
        (string? ClassName, string? MethodName) classAndMethodnames = exception.GetActualMethodAndClassName();

        using var scope = _scopeFactory.CreateScope();
        scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerRepository>();

        string exceptionMessage = exception switch
        {
            { InnerException: not null } => Convert.ToString(exception.InnerException) ?? string.Empty,
            { Message: { Length: > 0 } } => exception.Message,
            { StackTrace: { Length: > 0 } } => exception.StackTrace,
            _ => string.Empty
        };

        ////It will log exception
        loggerService.LogException(classAndMethodnames.ClassName ?? string.Empty, classAndMethodnames.MethodName ?? string.Empty, exceptionMessage);

        // In Development, let Developer Exception Page show stack trace
        if (_env.IsDevelopment())
        {
            // Rethrow exception to allow DeveloperExceptionPage to handle it
            ExceptionDispatchInfo.Capture(exception).Throw();
        }

        return await ValueTask.FromResult(false);
    }
}

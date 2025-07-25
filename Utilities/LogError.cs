using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities.Interfaces;

namespace Utilities;

public class LogError(BookingManagementCmsContext _context,
                    IRemoteHostIpAddress _remoteHostAddress,
                    IHttpContextAccessor _contextAccessor) : ILogError
{
    /// <summary>
    /// It will log the debug info
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="message"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task LogDebugAsync(string source, string eventCode, string message, CancellationToken ct = default)
    {
        await SaveEventAsync("D", source, eventCode, message, ct);
    }

    /// <summary>
    /// It will log the exception
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="additionalMessage"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task LogExceptionAsync(string source, string eventCode, string? additionalMessage = null, CancellationToken ct = default)
    {
        await SaveEventAsync("E", source, eventCode, additionalMessage, ct);
    }

    /// <summary>
    /// It will log the information
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="eventDescription"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task LogInformationAsync(string source, string eventCode, string eventDescription = "", CancellationToken ct = default)
    {
        await SaveEventAsync("I", source, eventCode, eventDescription, ct);
    }

    /// <summary>
    /// It will log the stack trace
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="message"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task LogTraceAsync(string source, string eventCode, string message, CancellationToken ct)
    {
        await SaveEventAsync("T", source, eventCode, message, ct);
    }

    /// <summary>
    /// It will log the warnings
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="additionalMessage"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task LogWarningAsync(string source, string eventCode, string additionalMessage, CancellationToken ct)
    {
        await SaveEventAsync("W", source, eventCode, additionalMessage, ct);
    }

    /// <summary>
    /// Save the event information
    /// </summary>
    /// <param name="EventType"></param>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SaveEventAsync(string EventType, string source, string eventCode, string? message, CancellationToken ct)
    {
        //Get Current context
        var httpContext = _contextAccessor.HttpContext;

        //Returns logger entity obj
        var errorDetails = new Logger
        {
            EventType = EventType,
            EventTime = DateTime.Now,
            Source = source,
            EventCode = eventCode,
            UserName = "public",
            Ipaddress = _remoteHostAddress.GetRemoteHostIpAddress(httpContext) ?? string.Empty,
            EventDescription = message ?? string.Empty,
            EventUrl = Convert.ToString(httpContext.Request.Path.Value) ?? string.Empty,
            EventMachineName = Environment.MachineName
        };

        await _context.Loggers.AddAsync(errorDetails);
        await _context.SaveChangesAsync(ct);
    }
}

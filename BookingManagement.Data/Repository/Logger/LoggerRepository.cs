using Data.EDMX;
using Data.Interfaces.IpAddress;
using Data.Interfaces.Logger;
using Microsoft.AspNetCore.Http;

namespace Data.Repository.Logger;

public class LoggerRepository(ApplicationDbContext _context,
                              IRemoteHostIpAddress _remoteHostAddress,
                              IHttpContextAccessor _contextAccessor) : ILoggerRepository
{
    /// <summary>
    /// It will log the debug info
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="message"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void LogDebug(string source, string eventCode, string message)
    {
        var eventinfo = BindEventDto("D", source, eventCode, message);
        _context.EventLog.Add(eventinfo);
        _context.SaveChanges();
    }

    /// <summary>
    /// It will log the exception
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="additionalMessage"></param>
    /// <param name="siteId"></param>
    public void LogException(string source, string eventCode, string? additionalMessage = null)
    {
        var eventinfo = BindEventDto("E", source, eventCode, additionalMessage);
        _context.EventLog.Add(eventinfo);
        _context.SaveChanges();
    }

    /// <summary>
    /// It will log the information
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="eventDescription"></param>
    public void LogInformation(string source, string eventCode, string eventDescription = "")
    {
        var eventinfo = BindEventDto("I", source, eventCode, eventDescription);
        _context.EventLog.Add(eventinfo);
        _context.SaveChanges();
    }

    /// <summary>
    /// It will log the stack trace
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="message"></param>
    public void LogTrace(string source, string eventCode, string message)
    {
        var eventinfo = BindEventDto("T", source, eventCode, message);
        _context.EventLog.Add(eventinfo);
        _context.SaveChanges();
    }

    /// <summary>
    /// It will log the warnings
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="additionalMessage"></param>
    /// <param name="siteId"></param>
    public void LogWarning(string source, string eventCode, string additionalMessage)
    {
        var eventinfo = BindEventDto("W", source, eventCode, additionalMessage);
        _context.EventLog.Add(eventinfo);
        _context.SaveChanges();
    }

    /// <summary>
    /// It will bing infomation to event entity
    /// </summary>
    /// <param name="EventType"></param>
    /// <param name="source"></param>
    /// <param name="eventCode"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public BookingManagemnetEventLog BindEventDto(string EventType, string source, string eventCode, string? message)
    {
        //Get Current context
        var httpContext = _contextAccessor.HttpContext;

        //Returns logger entity obj
        return new()
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
    }
}

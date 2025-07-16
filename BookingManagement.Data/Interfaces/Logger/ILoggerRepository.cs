using Data.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.Logger;

public interface ILoggerRepository
{
    void LogInformation(string source, string eventCode, string eventDescription = "");
    void LogWarning(string source, string eventCode, string additionalMessage);
    void LogException(string source, string eventCode, string? additionalMessage = null);
    void LogDebug(string source, string eventCode, string message);
    void LogTrace(string source, string eventCode, string message);
    BookingManagemnetEventLog BindEventDto(string EventType, string source, string eventCode, string? message);
}

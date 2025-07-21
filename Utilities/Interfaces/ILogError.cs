using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Interfaces;

public interface ILogError
{
    Task LogInformationAsync(string source, string eventCode, string eventDescription = "",CancellationToken ct = default);
    Task LogWarningAsync(string source, string eventCode, string additionalMessage, CancellationToken ct);
    Task LogExceptionAsync(string source, string eventCode, string? additionalMessage = null, CancellationToken ct = default);
    Task LogDebugAsync(string source, string eventCode, string message, CancellationToken ct);
    Task LogTraceAsync(string source, string eventCode, string message, CancellationToken ct);
    Task SaveEventAsync(string EventType, string source, string eventCode, string? message, CancellationToken ct);
}

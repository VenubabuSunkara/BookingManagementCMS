using Microsoft.AspNetCore.Http;

namespace Utilities.Interfaces;

public interface IRemoteHostIpAddress
{
    string? GetRemoteHostIpAddress(HttpContext httpContext);
}

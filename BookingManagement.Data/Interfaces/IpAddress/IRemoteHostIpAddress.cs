using Microsoft.AspNetCore.Http;

namespace Data.Interfaces.IpAddress;

public interface IRemoteHostIpAddress
{
    string? GetRemoteHostIpAddress(HttpContext httpContext);
}

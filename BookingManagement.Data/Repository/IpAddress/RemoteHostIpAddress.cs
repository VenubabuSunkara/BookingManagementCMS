using Data.Interfaces.IpAddress;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IpAddress;

public class RemoteHostIpAddress : IRemoteHostIpAddress
{
    /// <summary>
    /// IP address of the client that is making a request to a web server
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public string? GetRemoteHostIpAddress(HttpContext httpContext)
    {
        var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

        if (!string.IsNullOrEmpty(forwardedFor))
        {
            var ips = forwardedFor.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(s => s.Trim());
            foreach (var ip in ips)
            {
                if (IPAddress.TryParse(ip, out var address) &&
                    (address.AddressFamily is AddressFamily.InterNetwork
                     or AddressFamily.InterNetworkV6))
                {
                    return Convert.ToString(address) ?? string.Empty;
                }
            }
        }

        if (httpContext.Request.Headers.TryGetValue("X-Real-IP", out var xRealIp))
        {
            if (!IPAddress.TryParse(xRealIp, out IPAddress? address))
            {
                return null;
            }
            var isValidIP = (address.AddressFamily is AddressFamily.InterNetwork
                             or AddressFamily.InterNetworkV6);

            if (isValidIP)
            {
                return Convert.ToString(address);
            }
        }

        var remoteIp = httpContext.Connection.RemoteIpAddress;
        if (remoteIp != null &&
            (remoteIp.AddressFamily == AddressFamily.InterNetwork ||
             remoteIp.AddressFamily == AddressFamily.InterNetworkV6))
        {
            return Convert.ToString(remoteIp) ?? string.Empty;
        }

        return string.Empty;

    }
}

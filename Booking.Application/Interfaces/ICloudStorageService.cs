using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface ICloudStorageService
    {
        Task UploadAsync(string containerName, string objectName, Stream data);
        Task DeleteAsync(string containerName, string objectName);
        Task<Stream> DownloadAsync(string containerName, string objectName);
    }
}

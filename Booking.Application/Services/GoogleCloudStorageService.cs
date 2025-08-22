using Booking.Application.Interfaces;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class GoogleCloudStorageService(IConfiguration config) : ICloudStorageService
    {
        private readonly StorageClient _client = StorageClient.Create();
        private readonly string _bucket = config["GCP:BucketName"];

        public async Task DeleteAsync(string containerName, string objectName)
        {
            await _client.DeleteObjectAsync(containerName, objectName);
        }

        public async Task<Stream> DownloadAsync(string containerName, string objectName)
        {
            var memoryStream = new MemoryStream();
            await _client.DownloadObjectAsync(containerName, objectName, memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task UploadAsync(string containerName, string objectName, Stream data)
        {
            await _client.UploadObjectAsync(_bucket, objectName, null, data);
        }
    }
}

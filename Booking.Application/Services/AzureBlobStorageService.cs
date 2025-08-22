using Azure.Storage.Blobs;
using Booking.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class AzureBlobStorageService : ICloudStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public AzureBlobStorageService(IConfiguration config)
        {
            _blobServiceClient = new BlobServiceClient(config["Azure:ConnectionString"]);
        }
        public async Task DeleteAsync(string containerName, string objectName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blob = container.GetBlobClient(objectName);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<Stream> DownloadAsync(string containerName, string objectName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blob = container.GetBlobClient(objectName);
            var downloadInfo = await blob.DownloadAsync(); // Returns BlobDownloadInfo with content stream :contentReference[oaicite:1]{index=1}
            return downloadInfo.Value.Content;
        }

        public async Task UploadAsync(string containerName, string objectName, Stream data)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            await container.UploadBlobAsync(objectName, data);
        }
    }
}

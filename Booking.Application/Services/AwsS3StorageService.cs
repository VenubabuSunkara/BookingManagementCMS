using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Booking.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class AwsS3StorageService(IAmazonS3 s3Client, IConfiguration config) : ICloudStorageService
    {
        private readonly IAmazonS3 _s3Client = s3Client;
        private readonly string _bucket = config["AWS:BucketName"];

        public async Task DeleteAsync(string containerName, string objectName)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = containerName,
                Key = objectName
            };
            await _s3Client.DeleteObjectAsync(request);
        }

        public async Task<Stream> DownloadAsync(string containerName, string objectName)
        {
            var response = await _s3Client.GetObjectAsync(containerName, objectName);
            var memoryStream = new MemoryStream();
            await response.ResponseStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream; // Returns full content as stream
        }

        public async Task UploadAsync(string containerName, string objectName, Stream data)
        {
            var util = new TransferUtility(_s3Client);
            await util.UploadAsync(data, _bucket, objectName);
        }
    }
}

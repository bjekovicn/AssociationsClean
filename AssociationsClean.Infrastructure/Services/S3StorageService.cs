

using Amazon.S3;
using Amazon.S3.Transfer;
using AssociationsClean.Application.Shared.Abstractions.Storage;
using Microsoft.Extensions.Options;

namespace AssociationsClean.Infrastructure.Services
{
    public class S3StorageService : IStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly S3Settings _s3Settings;

        public S3StorageService(IAmazonS3 s3Client, IOptions<S3Settings> options)
        {
            _s3Client = s3Client;
            _s3Settings = options.Value;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileStream,
                Key = fileName,
                BucketName = _s3Settings.BucketName,
                ContentType = contentType
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);

            return $"https://{_s3Settings.BucketName}.s3.amazonaws.com/{fileName}";
        }

        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            var response = await _s3Client.GetObjectAsync(_s3Settings.BucketName, fileName);
            return response.ResponseStream;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            await _s3Client.DeleteObjectAsync(_s3Settings.BucketName, fileName);
        }
    }
}

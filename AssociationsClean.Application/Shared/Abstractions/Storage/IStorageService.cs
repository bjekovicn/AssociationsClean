

namespace AssociationsClean.Application.Shared.Abstractions.Storage
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
        Task<Stream> DownloadFileAsync(string fileName);
        Task DeleteFileAsync(string fileName);
    }

}

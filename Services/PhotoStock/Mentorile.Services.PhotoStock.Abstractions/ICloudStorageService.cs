namespace Mentorile.Services.PhotoStock.Abstractions;
public interface ICloudStorageService
{
    Task<UploadResult> UploadAsync(byte[] content, string fileName, string contentType);    
    Task<bool> DeleteAsync(string storagePath);
}
using System.Net.Http.Headers;
using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.PhotoStocks;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services;
public class PhotoStockService : IPhotoStockService
{
    private const string controllersName = "photos";
    private readonly HttpClient _httpClient;

    public PhotoStockService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> DeletePhotoAsync(string photoUri)
    {
        var response = await _httpClient.DeleteAsync($"{controllersName}/{photoUri}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> HardDeleteAsync(string photoUri)
    {
        var response = await _httpClient.DeleteAsync($"{controllersName}/hard/{photoUri}");
        return response.IsSuccessStatusCode;
    }

    public async Task<Result<PhotoViewModel>> UploadPhotoAsync(IFormFile formFile, bool isPublic = false)
    {
        if (formFile == null || formFile.Length <= 0) return null;

        using var memoryStream = new MemoryStream();
        await formFile.CopyToAsync(memoryStream);
        var multipartContent = new MultipartFormDataContent();
        var byteArrayContent = new ByteArrayContent(memoryStream.ToArray());
        byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);
        byteArrayContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = "\"file\"",
            FileName = $"\"{formFile.FileName}\""
        };

        multipartContent.Add(byteArrayContent, "file", formFile.FileName);
        var response = await _httpClient.PostAsync($"{controllersName}?isPublic={isPublic}", multipartContent);
        if (!response.IsSuccessStatusCode) return null;
        var model = await response.Content.ReadFromJsonAsync<Result<PhotoViewModel>>(); 
        
        return model;
    }
}
using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.PhotoStocks;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services;
public class PhotoStockService : IPhotoStockService
{
    private readonly HttpClient _httpClient;

    public PhotoStockService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> DeletePhotoAsync(string photoUri)
    {
        var response = await _httpClient.DeleteAsync($"photos?photoUri={photoUri}");
        return response.IsSuccessStatusCode;
    }

    public async Task<Result<PhotoViewModel>> UploadPhotoAsync(IFormFile formFile)
    {
        if(formFile == null || formFile.Length <= 0) return null;

        // example 2039453243242.jpg
        var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(formFile.FileName)}";
        using var memoryStream = new MemoryStream();
        await formFile.CopyToAsync(memoryStream);
        var multipartContent = new MultipartFormDataContent();
        multipartContent.Add(new ByteArrayContent(memoryStream.ToArray()), "photo", randomFileName);
        var response = await _httpClient.PostAsync("photos", multipartContent);
        if(!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<Result<PhotoViewModel>>();
    }
}
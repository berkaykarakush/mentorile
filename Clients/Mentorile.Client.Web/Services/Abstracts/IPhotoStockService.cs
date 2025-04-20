using Mentorile.Client.Web.ViewModels.PhotoStocks;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IPhotoStockService
{
    Task<Result<PhotoViewModel>> UploadPhotoAsync(IFormFile formFile);    
    Task<bool> DeletePhotoAsync(string photoUri);
}
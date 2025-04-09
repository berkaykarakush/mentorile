using Mentorile.Services.PhotoStock.DTOs;
using Mentorile.Shared.Common;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.PhotoStock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : CustomControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Save(IFormFile photo, CancellationToken cancellationToken)
    {
        if(photo == null && photo.Length < 0) return CreateActionResultInstance(Result<PhotoDTO>.Failure("Photo is empty."));

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
        using var stream = new FileStream(path, FileMode.Create);
        await photo.CopyToAsync(stream, cancellationToken);
        
        var returnPath = $"photos/{photo.FileName}";
        var photoDTO = new PhotoDTO() { Uri = returnPath };
        return CreateActionResultInstance(Result<PhotoDTO>.Success(photoDTO));
    }    

    [HttpDelete]
    public async Task<IActionResult> Delete(string photoUri)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUri);

        if(!System.IO.File.Exists(path)) return CreateActionResultInstance(Result<PhotoDTO>.Failure("Photo not found."));

        System.IO.File.Delete(path);
        return CreateActionResultInstance(Result<string>.Success("Photo deleted."));
    }
}
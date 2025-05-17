using MediatR;
using Mentorile.Services.PhotoStock.Application.Commands;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.PhotoStock.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public PhotosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, [FromQuery] bool isPublic = false)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var fileBytes = ms.ToArray();

        var command = new UploadPhotoCommand()
        {
            Content = fileBytes,
            FileName = file.FileName,
            ContentType = file.ContentType,
            IsPublic = isPublic
        };
        return CreateActionResultInstance(await _mediator.Send(command));
    }
    [HttpDelete("{photoId}")]
    public async Task<IActionResult> Delete(string photoId)
        => CreateActionResultInstance(await _mediator.Send(new DeletePhotoCommand{ PhotoId = photoId }));

    [HttpDelete("hard/{photoId}")]
    public async Task<IActionResult> HardDelete(string photoId)
        => CreateActionResultInstance(await _mediator.Send(new HardDeletePhotoCommand{ PhotoId = photoId }));
}
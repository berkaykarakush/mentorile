using Mentorile.Client.Web.Extensions;
using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Notes;
using Mentorile.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.Web.Controllers;

[Route("notes")]
public class NotesController : Controller
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(PagingParams pagingParams, CancellationToken cancellationToken)
        => await LoadNotesAsync(() => _noteService.GetAllAsync(pagingParams, cancellationToken), pagingParams);
        // TODO: Refactore this section
        // => await this.LoadPagedAsync(
        //     () => _noteService.GetAllAsync(pagingParams, cancellationToken),
        //     pagingParams,
        //     viewName: "Index",
        //     emptyMessage: "No notes available.");

    [HttpGet("my")]
    public async Task<IActionResult> UserIndex(PagingParams pagingParams, CancellationToken cancellationToken)
        => await LoadNotesAsync(() => _noteService.GetAllByUserIdAsync(pagingParams, cancellationToken), pagingParams);
        // TODO: Refactore this section
        // => await this.LoadPagedAsync(
        //     () => _noteService.GetAllByUserIdAsync(pagingParams, cancellationToken),
        //     pagingParams,
        //     viewName: "Index",
        //     emptyMessage: "No notes available.");

    [HttpGet("create")]
    public IActionResult Create() => View();

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateNoteInput input, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(input);

        var result = await _noteService.CreateAsync(input, cancellationToken);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message ?? "Something went wrong.");
            return View(input);
        }

        return RedirectToAction(nameof(UserIndex));
    }

    [HttpGet("update")]
    public async Task<IActionResult> Update(Guid noteId)
    {
        var note = await _noteService.GetByIdAsync(noteId);
        if (note == null) return RedirectToAction(nameof(UserIndex));

        var model = new UpdateNoteInput
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content
        };

        return View(model);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateNoteInput input, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(input);

        var result = await _noteService.UpdateAsync(input, cancellationToken);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message ?? "Update failed.");
            return View(input);
        }

        return RedirectToAction(nameof(UserIndex));
    }

    [HttpGet("delete/{noteId}")]
    public async Task<IActionResult> Delete(Guid noteId, CancellationToken cancellationToken)
    {
        var result = await _noteService.DeleteAsync(noteId, cancellationToken);
        return RedirectToAction(nameof(UserIndex));
    }

    private async Task<IActionResult> LoadNotesAsync(
        Func<Task<PagedResult<NoteViewModel>>> loader,
        PagingParams pagingParams)
    {
        var result = await loader();

        ViewBag.Paging = pagingParams;

        if (!result.IsSuccess || result.Data == null)
        {
            var emptyResult = PagedResult<NoteViewModel>.Create(
                new List<NoteViewModel>(), 0, pagingParams, 200, "No notes available.");
            return View("Index", emptyResult);
        }

        var pagedResult = PagedResult<NoteViewModel>.Create(
            result.Data.ToList(),
            result.TotalCount,
            pagingParams,
            200,
            "Success"
        );

        return View("Index", pagedResult);
    }
}

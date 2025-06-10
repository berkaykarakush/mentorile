using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Notes;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services;

namespace Mentorile.Client.Web.Services;
public class NoteService : INoteService
{
    private readonly HttpClient _httpClient;
    private readonly ISharedIdentityService _sharedIdentityService;
    public NoteService(HttpClient httpClient, ISharedIdentityService sharedIdentityService)
    {
        _httpClient = httpClient;
        _sharedIdentityService = sharedIdentityService;
    }

    private const string controllersName = "notes";
    public async Task<Result<bool>> CreateAsync(CreateNoteInput input, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{controllersName}", input, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Hata: {response.StatusCode}");
            Console.WriteLine($"Detay: {errorContent}");
        }
        var result = await response.Content.ReadFromJsonAsync<Result<bool>>();
        return result;
    }

    public async Task<Result<bool>> UpdateAsync(UpdateNoteInput input, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"{controllersName}/update/{input.Id}", input, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Hata: {response.StatusCode}");
            Console.WriteLine($"Detay: {errorContent}");
        }
        var result = await response.Content.ReadFromJsonAsync<Result<bool>>();
        return result;
    }

    public async Task<Result<bool>> DeleteAsync(Guid noteId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"{controllersName}/delete/{noteId}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Hata: {response.StatusCode}");
            Console.WriteLine($"Detay: {errorContent}");
        }
        var result = await response.Content.ReadFromJsonAsync<Result<bool>>();
        return result;
    }

    public async Task<PagedResult<NoteViewModel>> GetAllAsync(PagingParams pagingParams, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{controllersName}", cancellationToken);
        if (!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<PagedResult<NoteViewModel>>>();
        return result.Data;
    }

    public async Task<PagedResult<NoteViewModel>> GetAllByUserIdAsync(PagingParams pagingParams, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{controllersName}/me", cancellationToken);
        if (!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<PagedResult<NoteViewModel>>>();
        return result.Data;
    }

    public async Task<NoteViewModel> GetByIdAsync(Guid noteId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{controllersName}/{noteId}", cancellationToken);
        if (!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<NoteViewModel>>();
        return result.Data;
    }
}
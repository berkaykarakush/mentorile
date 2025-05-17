namespace Mentorile.Services.PhotoStock.Abstractions;
public class UploadResult
{
    public string Uri { get; set; } = default!;
    public string? StoragePath { get; set; }
}
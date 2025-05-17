namespace Mentorile.Services.PhotoStock.Application.DTOs;
public class PhotoDTO
{
    public string Id { get; set; } 
    public string? UserId { get; set; }
    public string? PublicUri { get; set; }
    public string FileName { get; set; }
    public string? StoragePath { get; set; }
    public string ContentType { get; set; }
    public bool IsPublic { get; set; }
}
using System.Text.RegularExpressions;
using Mentorile.Client.Web.Settings;

namespace Mentorile.Client.Web.Helpers;

public class PhotoHelper
{
    private static string ExtractFileId(string photoUri)
    {
        if(string.IsNullOrEmpty(photoUri)) return null;
        var match = Regex.Match(photoUri, @"drive\.google\.com\/file\/d\/([^\/]+)\/", RegexOptions.IgnoreCase);
        if (!match.Success) return photoUri;

        return match.Success ? match.Groups[1].Value : null;
    }

    public string GetDriveThumbnailUri(string photoUri)
    {
        var fileId = ExtractFileId(photoUri);
        return fileId != null ? $"https://drive.google.com/thumbnail?id={fileId}" : null;
    }

    public string GetDriveDirectImageUri(string photoUri)
    {
        var fileId = ExtractFileId(photoUri);
        return fileId != null ? $"https://drive.google.com/uc?export=view&id={fileId}" : null;
    }
}
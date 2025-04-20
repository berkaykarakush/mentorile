using Mentorile.Client.Web.Settings;

namespace Mentorile.Client.Web.Helpers;
public class PhotoHelper
{
    private readonly IServiceApiSettings _serviceApiSettings;

    public PhotoHelper(IServiceApiSettings serviceApiSettings)
    {
        _serviceApiSettings = serviceApiSettings;
    }

    public string GetPhotoStockUri(string photoUri) => $"{_serviceApiSettings.PhotoStockUri}/photos/{photoUri}";
}
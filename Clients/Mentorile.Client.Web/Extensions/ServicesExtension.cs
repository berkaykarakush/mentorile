using Mentorile.Client.Web.Handlers;
using Mentorile.Client.Web.Services;
using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.Settings;

namespace Mentorile.Client.Web.Extensions;
public static class ServicesExtension
{
    public static void AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IIdentityService, IdentityService>();

        services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
        services.AddScoped<ClientCredentialsTokenHandler>();

        var serviceApiSettings = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
        services.AddHttpClient<ICourseService, CourseService>(opt => {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Course.Path}"); 
        }).AddHttpMessageHandler<ClientCredentialsTokenHandler>();

        services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt => {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.PhotoStock.Path}"); 
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IBasketService, BasketService>(opt => {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Basket.Path}"); 
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IDiscountService, DiscountService>(opt => {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Discount.Path}"); 
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IPaymentService, PaymentService>((sp, opt) => {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Payment.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IOrderService, OrderService>((sp, opt) => {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Order.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IStudyService, StudyService>((sp, opt) => {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Study.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IUserService, UserService>((sp, opt) => {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.User.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
    }   
}
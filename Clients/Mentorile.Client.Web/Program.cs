using FluentValidation;
using FluentValidation.AspNetCore;
using Mentorile.Client.Web.Extensions;
using Mentorile.Client.Web.Handlers;
using Mentorile.Client.Web.Helpers;
using Mentorile.Client.Web.Settings;
using Mentorile.Client.Web.Validators.Courses;
using Mentorile.Shared.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// FluentValidation yapılandırması
builder.Services.AddControllersWithViews(options =>
{
    // Nullable olmayan referans türleri için varsayılan [Required] davranışını devre dışı bırakır
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

// FluentValidation servislerini ekler
builder.Services.AddFluentValidationAutoValidation(); // Otomatik model doğrulama
builder.Services.AddFluentValidationClientsideAdapters(); // İstemci tarafı doğrulama adaptörleri

// Belirli bir assembly'den tüm validator'ları kaydeder
builder.Services.AddValidatorsFromAssemblyContaining<CreateCourseInputValidator>();

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
builder.Services.AddSingleton<IServiceApiSettings>(sp =>
    sp.GetRequiredService<IOptions<ServiceApiSettings>>().Value);

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.AddSingleton<IClientSettings>(sp =>
    sp.GetRequiredService<IOptions<ClientSettings>>().Value);

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddSingleton<PhotoHelper>();

builder.Services.AddMemoryCache();
builder.Services.AddHttpClientServices(builder.Configuration);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>{
    opts.LoginPath = "/Auth/SignIn";
    opts.ExpireTimeSpan = TimeSpan.FromDays(60);
    opts.SlidingExpiration = true;
    opts.Cookie.Name = "mentorile.web.cookie";
});

builder.WebHost.UseUrls("http://+:80");

var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Home/Index");
        return;
    }
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()){
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
} 

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

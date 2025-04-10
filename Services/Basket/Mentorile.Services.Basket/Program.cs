using Mentorile.Services.Basket.Services;
using Mentorile.Services.Basket.Settings;
using Mentorile.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Basket API", Version = "v1" });
});

// RedisSettings'i yapılandır
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));

// IRedisSettings olarak da resolve edilebilmesi için ayrı bir ekleme:
builder.Services.AddSingleton<IRedisSettings>(sp =>
    sp.GetRequiredService<IOptions<RedisSettings>>().Value);

// ConnectionMultiplexer'ı ekle
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    return ConnectionMultiplexer.Connect($"{config.Host}:{config.Port}");
});

// Redis servisi
builder.Services.AddSingleton<IRedisService, RedisService>();
builder.Services.AddScoped<IBasketService, BasketService>();

// Identity ve context
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ISharedIdentityService, SharedIdentityService>();

// Authorization
var requireAuthorizePolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
builder.Services.AddControllers(opt => 
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

// JWT Authentication
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); // sub degerini nameidentifier'a donusumu engelle
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
{
    options.Authority = builder.Configuration["Authority"];
    options.Audience = builder.Configuration["Audience"];
    options.RequireHttpsMetadata = false;
});

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
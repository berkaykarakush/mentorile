using MassTransit;
using Mentorile.Services.Basket.Application.Consumers;
using Mentorile.Services.Basket.Domain.Interfaces;
using Mentorile.Services.Basket.Infrastructure.Persistence;
using Mentorile.Services.Basket.Infrastructure.Services;
using Mentorile.Services.Basket.Infrastructure.Settings;
using Mentorile.Shared.Abstractions;
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
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Mentorile.Services.Basket.Application.CommandHandlers.AddItemToBasketCommandHandler).Assembly);
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception}");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddMassTransit(x => 
{
    x.AddConsumer<CourseNameChangedEventConsumer>();

    // default port 5672
    x.UsingRabbitMq((context, configuration) => 
    {
        configuration.Host(builder.Configuration["RabbitMQUri"], "/", host => 
        {
            // default settings
            host.Username("guest");
            host.Password("guest");
        });

        configuration.ReceiveEndpoint("course-name-changed-event-queue", e => {
            e.ConfigureConsumer<CourseNameChangedEventConsumer>(context);
        });
    });
});

builder.WebHost.UseUrls("http://+:80");

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
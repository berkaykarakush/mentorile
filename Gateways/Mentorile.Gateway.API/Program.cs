using MassTransit;
using Mentorile.Gateway.API.Handlers;
using Mentorile.Gateway.API.Services;
using Mentorile.Gateway.API.Services.Abstracts;
using Mentorile.Shared.Messages.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Gateway API", Version = "v1" });
});

// Ocelot configuration
builder.Configuration
        .AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

builder.Services.AddScoped<IUserAccessService, UserAccessService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AccessControlDelegatingHandler>();


builder.Services.AddMassTransit(x =>
{
    x.AddRequestClient<UserAccessCheckQuery>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUri"], "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });

});

// Authorization
// var requireAuthorizePolicy = new AuthorizationPolicyBuilder()
//     .RequireAuthenticatedUser()
//     .Build();
// builder.Services.AddControllers(opt => 
// {
//     opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
// });

// JWT Authentication
// JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); // sub degerini nameidentifier'a donusumu engelle
builder.Services
    .AddAuthentication("GatewayAuthenticationScheme")
    .AddJwtBearer("GatewayAuthenticationScheme",options => 
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


builder.WebHost.UseUrls("http://+:80");

builder.Services.AddOcelot(builder.Configuration).AddDelegatingHandler<AccessControlDelegatingHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();
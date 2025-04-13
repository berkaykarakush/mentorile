using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
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
builder.Services.AddOcelot(builder.Configuration);

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
    });


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
using Mentorile.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

var builder = WebApplication.CreateBuilder(args);

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Payment API", Version = "v1" });
});

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
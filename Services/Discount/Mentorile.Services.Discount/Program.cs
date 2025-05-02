using Mentorile.Services.Discount.Services;
using Mentorile.Services.Discount.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

var builder = WebApplication.CreateBuilder(args);

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Discount API", Version = "v1" });
});


builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddScoped<IDiscountService, DiscountService>();

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

// Automapper
builder.Services.AddAutoMapper(typeof(Program));

builder.WebHost.UseUrls("http://+:80");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course API v1");
    });
}

// app.UseHttpsRedirection();
app.MapControllers(); // required for controllers/swagger

app.UseAuthentication();
app.UseAuthorization();

app.Run();
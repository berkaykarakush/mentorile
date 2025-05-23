using Mentorile.Services.PhotoStock.Abstractions;
using Mentorile.Services.PhotoStock.Domain.Interfaces;
using Mentorile.Services.PhotoStock.Infrastructure.Persistence;
using Mentorile.Services.PhotoStock.Infrastructure.Repositories;
using Mentorile.Services.PhotoStock.Infrastructure.Services;
using Mentorile.Services.PhotoStock.Infrastructure.Settings;
using Mentorile.Shared.Services;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

var builder = WebApplication.CreateBuilder(args);

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PhotoStock API", Version = "v1" });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), cfg => {
        cfg.MigrationsAssembly("Mentorile.Services.PhotoStock.Infrastructure");
    }));

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Mentorile.Services.PhotoStock.Application.CommandHandlers.UploadPhotoCommandHandler).Assembly);
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
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception}");
            return Task.CompletedTask;
        }
    };
});

// builder.Services.AddMassTransit(x => 
// {
//     x.AddConsumer<CreateOrderMessageCommandConsumer>();
//     x.AddConsumer<CourseNameChangedEventConsumer>();

//     // default port 5672
//     x.UsingRabbitMq((context, configuration) => 
//     {
//         configuration.Host(builder.Configuration["RabbitMQUri"], "/", host => 
//         {
//             // default settings
//             host.Username("guest");
//             host.Password("guest");
//         });

//         configuration.ReceiveEndpoint("create-order-service", e => {
//             e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
//         });

//         configuration.ReceiveEndpoint("course-name-changed-event-queue", e => {
//             e.ConfigureConsumer<CourseNameChangedEventConsumer>(context);
//         });
//     });
// });

builder.WebHost.UseUrls("http://+:80");

// Dependency Injection
builder.Services.Configure<GoogleDriveSettings>(builder.Configuration.GetSection("GoogleDriveSettings"));
builder.Services.AddSingleton<IGoogleDriveSettings>(sp => sp.GetRequiredService<IOptions<GoogleDriveSettings>>().Value);
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<ICloudStorageService, CloudStorageService>();
builder.Services.AddScoped<IExecutor, Executor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// automatic migrate database
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
dbContext.Database.Migrate();


// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
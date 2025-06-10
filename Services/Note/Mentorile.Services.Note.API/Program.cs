using MassTransit;
using MediatR;
using Mentorile.Services.Note.Domain.Interfaces;
using Mentorile.Services.Note.Infrastructure.Persistence;
using Mentorile.Services.Note.Infrastructure.Repositories;
using Mentorile.Shared.Behaviors;
using Mentorile.Shared.Services;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

var builder = WebApplication.CreateBuilder(args);

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1.0.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Note API", Version = "v1.0.0" });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), cfg => {
        cfg.MigrationsAssembly("Mentorile.Services.Note.Infrastructure");
}));

// Add MediatR
builder.Services.AddMediatR(cfg => { 
    cfg.RegisterServicesFromAssembly(typeof(Mentorile.Services.Note.Application.CommandHandlers.CreateNoteCommandHandler).Assembly); 
});
// Add LoggingBehavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

// Identity ve context
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ISharedIdentityService, SharedIdentityService>();

// Dependency Injection
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IExecutor, Executor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

builder.Services.AddMassTransit(x => {
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUri"], "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

builder.WebHost.UseUrls("http://+:80");

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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
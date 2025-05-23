using MediatR;
using Mentorile.Services.Email.Abstractions;
using Mentorile.Services.Email.Domain.Interfaces;
using Mentorile.Services.Email.Infrastructure.Persistence;
using Mentorile.Services.Email.Infrastructure.Repository;
using Mentorile.Services.Email.Infrastructure.Settings;
using Mentorile.Shared.Behaviors;
using Mentorile.Shared.Services;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Email API", Version = "v1.0.0" });
    });
}

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Mentorile.Services.Email.Application.CommandHandlers.SendManualEmailCommandHandler).Assembly);
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddSingleton<ISmtpSettings>(sp => sp.GetRequiredService<IOptions<SmtpSettings>>().Value);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), cfg => {
        cfg.MigrationsAssembly("Mentorile.Services.Email.Infrastructure");
}));

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IEmailLogRepository, EmailLogRepository>();
builder.Services.AddScoped<IExecutor, Executor>();

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

builder.Services.AddControllers(options => {
    options.Filters.Add(new AuthorizeFilter());
});

builder.WebHost.UseUrls("http://+:80");

var app = builder.Build();

// automatic migrate database
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
dbContext.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Email API v1");
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
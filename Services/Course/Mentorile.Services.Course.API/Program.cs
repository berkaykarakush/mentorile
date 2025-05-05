using MassTransit;
using MediatR;
using Mentorile.Services.Course.Domain.Interfaces;
using Mentorile.Services.Course.Infrastructure.Persistence;
using Mentorile.Services.Course.Infrastructure.Repositories;
using Mentorile.Services.Course.Infrastructure.Settings;
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
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Course API", Version = "v1.0.0" });
    });
}

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Mentorile.Services.Course.Application.CommandHandlers.CreateCourseCommandHandler).Assembly);
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), cfg => {
        cfg.MigrationsAssembly("Mentorile.Services.Course.Infrastructure");
}));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
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

builder.Services.AddMassTransit(x =>
        {
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


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course API v1");
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
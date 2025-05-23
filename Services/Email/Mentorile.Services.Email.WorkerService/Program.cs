using MassTransit;
using MediatR;
using Mentorile.Services.Email.Abstractions;
using Mentorile.Services.Email.Application.Consumers;
using Mentorile.Services.Email.Domain.Interfaces;
using Mentorile.Services.Email.Infrastructure.Persistence;
using Mentorile.Services.Email.Infrastructure.Repository;
using Mentorile.Services.Email.Infrastructure.Settings;
using Mentorile.Services.Email.WorkerService;
using Mentorile.Shared.Behaviors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IEmailLogRepository, EmailLogRepository>();

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddSingleton<ISmtpSettings>(sp => sp.GetRequiredService<IOptions<SmtpSettings>>().Value);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), cfg => {
        cfg.MigrationsAssembly("Mentorile.Services.Email.Infrastructure");
}));

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

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Mentorile.Services.Email.Application.CommandHandlers.SendManualEmailCommandHandler).Assembly);
});

builder.Services.AddMassTransit(x => 
{
    x.AddConsumer<OrderCreatedEventConsumer>();

    // default port 5672
    x.UsingRabbitMq((context, configuration) => 
    {
        configuration.Host(builder.Configuration["RabbitMQUri"], "/", host => 
        {
            // default settings
            host.Username("guest");
            host.Password("guest");
        });

        configuration.ReceiveEndpoint("order-created-event-queue", e => {
            e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
        });
    });
});

builder.Services.AddHttpClient("UserAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["UserAPI"]);
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

var host = builder.Build();

host.Run();
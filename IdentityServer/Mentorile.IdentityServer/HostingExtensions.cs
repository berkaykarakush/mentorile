using Duende.IdentityServer;
using MassTransit;
using MediatR;
using Mentorile.IdentityServer.Data;
using Mentorile.IdentityServer.Models;
using Mentorile.IdentityServer.Services;
using Mentorile.Shared.Behaviors;
using Mentorile.Shared.Services;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Mentorile.IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddRazorPages();
        builder.Services.AddLocalApiAuthentication();
        // Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "User API", Version = "v1.0.0" });
        });

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Şifre politikalarını burada yapılandırıyoruz
                options.Password.RequireDigit = true;            // En az bir rakam olmalı
                options.Password.RequireLowercase = true;       // En az bir küçük harf olmalı
                options.Password.RequireUppercase = true;       // En az bir büyük harf olmalı
                options.Password.RequireNonAlphanumeric = true; // En az bir özel karakter olmalı
                options.Password.RequiredLength = 8;                    // En az 8 karakter uzunluğunda olmalı
                options.Password.RequiredUniqueChars = 1;       // Benzersiz karakter sayısı
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationUser>()
            .AddResourceOwnerValidator<IdentityResourceOwnerPasswordValidator>();


        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = "copy client ID from Google here";
                options.ClientSecret = "copy client secret from Google here";
            });

        // builder.Services.AddHttpClient<IAuthService, AuthServie>(client => {
        //     client.BaseAddress = new Uri("http://identityserver.api/");
        // });


        builder.Services.AddMassTransit(x => 
        {   
            // default port 5672
            x.UsingRabbitMq((context, configuration) => 
            {
                configuration.Host(builder.Configuration["RabbitMQUri"], "/", host => 
                {
                    // default settings
                    host.Username("guest");
                    host.Password("guest");
                });
            });
        });

        // Add MediatR
        builder.Services.AddMediatR(cfg => { 
            cfg.RegisterServicesFromAssembly(typeof(Mentorile.IdentityServer.Handlers.RegisterUserCommandHandler).Assembly); 
        });
        // Add LoggingBehavior
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IExecutor, Executor>();

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();
        app.MapControllers();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}
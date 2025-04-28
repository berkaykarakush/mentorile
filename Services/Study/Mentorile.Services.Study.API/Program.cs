using Mentorile.Services.Study.Application.Interfaces;
using Mentorile.Services.Study.Application.Services;
using Mentorile.Services.Study.Domain.Services;
using Mentorile.Services.Study.Infrastructure.Persistence;
using Mentorile.Services.Study.Infrastructure.Repositories;
using Mentorile.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

var builder = WebApplication.CreateBuilder(args);

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Study API", Version = "v1" });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), cfg => {
        cfg.MigrationsAssembly("Mentorile.Services.Study.Infrastructure");
    }));

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Mentorile.Services.Study.Application.CommandHandlers.CreateStudyCommandHandler).Assembly);
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
builder.Services.AddScoped<IStudyAppService, StudyAppService>();
builder.Services.AddScoped<IStudyService, StudyRepository>();
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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

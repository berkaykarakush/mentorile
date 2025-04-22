using MassTransit;
using Mentorile.Services.Course.Models;
using Mentorile.Services.Course.Services;
using Mentorile.Services.Course.Settings;
using Mentorile.Shared.Messages.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// 'appsettings.json' dosyasındaki "DatabaseSettings" bölümünü IDatabaseSettings arayüzüne bağlar.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
// IDatabaseSettings'i singleton olarak kaydeder ve yapılandırmayı IOptions<DatabaseSettings> aracılığıyla alır.
builder.Services.AddSingleton<IDatabaseSettings>(ds => ds.GetRequiredService<IOptions<DatabaseSettings>>().Value);

// MongoDb Client ekle
builder.Services.AddScoped<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Course koleksiyonunu DI container'a ekle
builder.Services.AddScoped<IMongoCollection<Course>>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    var database = client.GetDatabase(settings.DatabaseName);
    return database.GetCollection<Course>(settings.CourseCollectionName);

});

// Service Registration
builder.Services.AddScoped<ICourseService, CourseService>();

// swagger configuration
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Course API", Version = "v1" });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
{
    options.Authority = builder.Configuration["Authority"];
    options.Audience = builder.Configuration["Audience"];
    options.RequireHttpsMetadata = false;
});

// required for controllers/swagger
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
}); 

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

builder.WebHost.UseUrls("http://+:80");

var app = builder.Build();

// Configure the HTTP request pipeline.
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
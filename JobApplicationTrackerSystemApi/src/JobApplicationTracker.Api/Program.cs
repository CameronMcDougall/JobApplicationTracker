using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using JobApplicationTracker.Api.MappingProfiles;
using JobApplicationTracker.Api.Repositories;
using JobApplicationTracker.Api.Services;
using JobApplicationTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    // opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services
    .AddScoped<IApplicationService, ApplicationService>()
    .AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile));
builder.Services.AddDbContext<JobApplicationTrackerDbContext>(
    options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);
builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobApplicationTracking.SystemApi", Version = "v1" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    // Remove 'Dto' suffix from models
    c.CustomSchemaIds(type => type.Name.Replace("Dto", ""));
});

var app = builder.Build();
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<JobApplicationTrackerDbContext>();

dbContext.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI(options => { options.SwaggerEndpoint("v1/swagger.json", "JobApplicationTracking.SystemApi v1"); });
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;
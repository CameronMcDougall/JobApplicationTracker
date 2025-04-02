using System.Reflection;
using JobApplicationTracker.Domain;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<JobApplicationTrackerDbContext>(
    _ =>
        builder.Configuration.GetConnectionString("DefaultConnection")
);

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

app.UseSwagger();
app.UseSwaggerUI(options => { options.SwaggerEndpoint("v1/swagger.json", "JobApplicationTracking.SystemApi v1"); });
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

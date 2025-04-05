using JobApplicationTracker.Api.MappingProfiles;
using JobApplicationTracker.Api.Repositories;
using JobApplicationTracker.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using FluentValidation;
using JobApplicationTracker.Api.Models.Requests;

namespace JobApplicationTracker.Api.Tests;

public class TestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        services.AddRouting();
        services
            .AddSingleton<IApplicationService, ApplicationService>()
            .AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddAutoMapper([typeof(ApplicationMappingProfile)]);
        services.AddValidatorsFromAssemblyContaining<AddApplicationRequestDtoValidator>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
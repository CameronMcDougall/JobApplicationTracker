using JobApplicationTracker.Api.Repositories;
using JobApplicationTracker.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplicationTracker.Api.Tests;

public class TestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers();

        services.AddRouting();
        services
            .AddSingleton<IApplicationService, ApplicationService>()
            .AddScoped<IApplicationRepository, ApplicationRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
    {
        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
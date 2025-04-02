using System.Data.Common;
using JobApplicationTracker.Domain;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplicationTracker.Api.Tests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        return WebHost.CreateDefaultBuilder();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(
                services =>
                {
                    // This is needed so it will map the routes in the solution
                    // Otherwise this will attempt to register the controllers in the test project instead
                    var assembly = typeof(Program).Assembly;
                    services.Remove(
                        services.SingleOrDefault(
                            d =>
                                d.ServiceType == typeof(DbContextOptions<JobApplicationTrackerDbContext>)
                        )!
                    );
                    services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType)!);
                    services.AddDbContext<JobApplicationTrackerDbContext>(
                        (_, option) => option.UseInMemoryDatabase("JobApplicationTracker")
                    );
                    services
                        .AddMvc(options => options.EnableEndpointRouting = false)
                        .AddApplicationPart(assembly);

                    services.BuildServiceProvider();
                }
            )
            .UseSolutionRelativeContentRoot("")
            .UseConfiguration(
                new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()
            )
            .UseTestServer()
            .UseStartup<TStartup>();
    }
}
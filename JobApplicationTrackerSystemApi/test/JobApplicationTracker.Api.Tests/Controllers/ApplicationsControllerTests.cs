using System.Net;
using System.Net.Http.Json;
using JobApplicationTracker.Api.Models.Responses;
using JobApplicationTracker.Api.Models.Shared;
using JobApplicationTracker.Domain;
using JobApplicationTracker.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ApplicationStatus = JobApplicationTracker.Domain.Models.Enums.ApplicationStatus;

namespace JobApplicationTracker.Api.Tests.Controllers;

public class ApplicationsControllerTests
{
    private const string ApplicationsBaseUrl = "applications";
    private readonly HttpClient _client;
    private readonly JobApplicationTrackerDbContext _dbContext;
    public ApplicationsControllerTests()
    {
        CustomWebApplicationFactory<TestStartup> factory = new();
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _dbContext = factory.Services.GetRequiredService<JobApplicationTrackerDbContext>();
    }

    [Fact]
    public async Task GetAsync_ValidApplication_ReturnsOK()
    {
        var application = new Application("Datacom",
            "Developer",
            ApplicationStatus.Interview,
            new DateTime(2025, 04, 02));
        _dbContext.Applications.Add(application);
        await _dbContext.SaveChangesAsync();

        var response = await _client.GetAsync($"{ApplicationsBaseUrl}/{application.Id}");
        
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseBody = await response.Content.ReadFromJsonAsync<GetApplicationResponseDto>();
        
        Assert.NotNull(responseBody);
        Assert.NotNull(responseBody.Application);

        Assert.Equal(application.AppliedDate, responseBody.Application.AppliedDate);
        Assert.Equal(application.CompanyName, responseBody.Application.CompanyName);
        Assert.Equal(application.Position, responseBody.Application.Position);
        Assert.Equal(ApplicationStatusDto.Interview, responseBody.Application.Status);
    }

    [Fact]
    public async Task GetAsync_NotExistantApplication_Returns404()
    {
        var application = new Application("Datacom",
            "Developer",
            ApplicationStatus.Interview,
            new DateTime(2025, 04, 02));
        _dbContext.Applications.Add(application);
        await _dbContext.SaveChangesAsync();

        var result = await _client.GetAsync($"{ApplicationsBaseUrl}/{application.Id}");
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound ,result.StatusCode);
    }
}
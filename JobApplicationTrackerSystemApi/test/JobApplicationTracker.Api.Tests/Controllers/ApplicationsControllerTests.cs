using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using JobApplicationTracker.Api.Models.Requests;
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
        _client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            }
        );
        _dbContext = factory.Services.GetRequiredService<JobApplicationTrackerDbContext>();
    }

    [Fact]
    public async Task GetApplication_ValidApplication_ReturnsOK()
    {
        var application = new Application(
            "Datacom",
            "Developer",
            ApplicationStatus.Interview,
            new DateTime(2025, 04, 01)
        );
        _dbContext.Applications.Add(application);
        await _dbContext.SaveChangesAsync();

        var response = await _client.GetAsync($"{ApplicationsBaseUrl}/{application.Id}");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseBody = await response.Content.ReadFromJsonAsync<GetApplicationResponseDto>(new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(responseBody);
        Assert.NotNull(responseBody.Application);

        Assert.Equal(application.AppliedDate, responseBody.Application.AppliedDate);
        Assert.Equal(application.CompanyName, responseBody.Application.CompanyName);
        Assert.Equal(application.Position, responseBody.Application.Position);
        Assert.Equal(ApplicationStatusDto.Interview, responseBody.Application.Status);
    }

    [Fact]
    public async Task GetApplication_NotExistantApplication_Returns404()
    {
        var result = await _client.GetAsync($"{ApplicationsBaseUrl}/{44}");
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task GetApplications_WithPagination_ReturnsOK()
    {

        for (var i = 0; i < 10; i++)
        {
            var application = new Application(
                "Datacom",
                "Developer",
                ApplicationStatus.Interview,
                new DateTime(2025, 04, 02)
            );
            _dbContext.Applications.Add(application);
        }

        await _dbContext.SaveChangesAsync();

        var result = await _client.GetAsync($"{ApplicationsBaseUrl}?pageSize=10&pageNumber=0");
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task PostAsync_NewApplication_Returns201()
    {
        var request = new AddApplicationRequestDto
        {
            Position = "Developer",
            CompanyName = "Datacom",
            Status = ApplicationStatusDto.Offer,
            DateApplied = new DateTime(2025, 04, 03)
        };

        var result = await _client.PostAsJsonAsync($"{ApplicationsBaseUrl}", request);
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);

        var application = _dbContext.Applications.FirstOrDefault(e => e.AppliedDate == request.DateApplied);
        Assert.NotNull(application);
        Assert.Equal(request.DateApplied, application.AppliedDate);
        Assert.Equal(request.Position, application.Position);
        Assert.Equal(request.CompanyName, application.CompanyName);
        Assert.Equal(ApplicationStatus.Offer, application.Status);
    }

    [Fact]
    public async Task PatchAsync_NewApplication_Returns204()
    {
        var application = new Application(
            "Datacom",
            "Developer",
            ApplicationStatus.Interview,
            new DateTime(2025, 04, 04)
        );
        _dbContext.Applications.Add(application);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateApplicationRequestDto
        {
            Status = ApplicationStatusDto.Offer
        };

        var result = await _client.PatchAsJsonAsync($"{ApplicationsBaseUrl}/{application.Id}", request);
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }
}
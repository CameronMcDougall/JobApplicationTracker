using AutoMapper;
using JobApplicationTracker.Api.MappingProfiles;
using JobApplicationTracker.Api.Models;
using JobApplicationTracker.Api.Models.Results.Enums;
using JobApplicationTracker.Api.Models.Shared;
using JobApplicationTracker.Api.Repositories;
using JobApplicationTracker.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using DatabaseApplicationStatus = JobApplicationTracker.Domain.Models.Enums.ApplicationStatus;
using DatabaseApplication = JobApplicationTracker.Domain.Models.Application;

namespace JobApplicationTracker.Api.Tests.Services;

public class ApplicationServiceTests_Unit
{
    [Theory]
    [InlineData(ApplicationStatus.Interview, DatabaseApplicationStatus.Interview)]
    [InlineData(ApplicationStatus.Offer, DatabaseApplicationStatus.Offer)]
    [InlineData(ApplicationStatus.Rejected, DatabaseApplicationStatus.Rejected)]
    public async Task UpdateApplication_GivenStatus_UpdatesApplication(
        ApplicationStatus givenStatus,
        DatabaseApplicationStatus expectedStatus
    )
    {
        const long id = 4L;
        var repo = new Mock<IApplicationRepository>();
        var mapper = new Mapper(
            new MapperConfiguration(cfg => cfg.AddProfiles([new ApplicationMappingProfile()]))
        );
        var mockLogger = new Mock<ILogger<ApplicationService>>();
        var service = new ApplicationService(repo.Object, mapper, mockLogger.Object);

        var result = await service.UpdateApplication(id, givenStatus, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(UpdateApplicationStatus.Success, result.Status);

        repo.Verify(e => e.UpdateApplication(id, expectedStatus, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetApplication_WithCorrectId_ReturnsCorrect()
    {
        var application = new DatabaseApplication(
            "Datacom",
            "Developer",
            DatabaseApplicationStatus.Interview,
            new DateTime(2025, 04, 02)
        );

        var mapper = new Mapper(
            new MapperConfiguration(cfg => cfg.AddProfiles([new ApplicationMappingProfile()]))
        );
        var mockLogger = new Mock<ILogger<ApplicationService>>();
        var repo = new Mock<IApplicationRepository>();
        repo
            .Setup(e => e.GetApplication(application.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(application);
        var service = new ApplicationService(repo.Object, mapper, mockLogger.Object);

        var result = await service.GetApplication(application.Id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(GetApplicationStatus.Success, result.Status);

        var resultApplication = result.Application;
        Assert.Equal(application.Id, resultApplication.Id);
        Assert.Equal(application.CompanyName, resultApplication.CompanyName);
        Assert.Equal(application.AppliedDate, resultApplication.AppliedDate);
        Assert.Equal(application.Position, resultApplication.Position);
        Assert.Equal(ApplicationStatus.Interview, resultApplication.Status);
    }

    [Fact]
    public void GetApplications_WithPaging_ReturnsCorrectSize()
    {
        var applications = new List<DatabaseApplication>();
        const int pageSize = 10;
        const int currentPage = 0;
        for (var i = 0; i < pageSize; i++)
        {
            var application = new DatabaseApplication(
                "Datacom" + i,
                "Developer",
                DatabaseApplicationStatus.Interview,
                new DateTime(2025, 04, 02)
            )
            {
                Id = i
            };

            applications.Add(application);
        }

        var repo = new Mock<IApplicationRepository>();
        repo
            .Setup(e => e.GetPaginatedApplications(pageSize, currentPage, PagingOrder.Ascending))
            .Returns(new PaginatedResult<DatabaseApplication>
            {
                Items = applications, 
                PagingInfo = new PagingInfo
                {
                    Current = 1,
                    TotalItems = pageSize,
                    TotalPages = 1
                }
            });
        var mapper = new Mapper(
            new MapperConfiguration(cfg => cfg.AddProfiles([new ApplicationMappingProfile()]))
        );
        var mockLogger = new Mock<ILogger<ApplicationService>>();
        var service = new ApplicationService(repo.Object, mapper, mockLogger.Object);

        var result = service.GetApplications(pageSize, currentPage, PagingOrder.Ascending);
        Assert.NotNull(result);
        Assert.Equal(GetApplicationsStatus.Success, result.Status);

        var resultApplications = result.Applications;
        Assert.Equal(pageSize, resultApplications.Count());
    }

    [Fact]
    public async Task AddApplication_Valid_AddsCorrectly()
    {
        var application = new DatabaseApplication(
            "Datacom",
            "Developer",
            DatabaseApplicationStatus.Interview,
            new DateTime(2025, 04, 02)
        );

        var repo = new Mock<IApplicationRepository>();
        var mockLogger = new Mock<ILogger<ApplicationService>>();
        var mapper = new Mapper(
            new MapperConfiguration(cfg => cfg.AddProfiles([new ApplicationMappingProfile()]))
        );
        var service = new ApplicationService(repo.Object, mapper, mockLogger.Object);

        var result = await service.AddApplication(
            application.CompanyName,
            application.Position,
            ApplicationStatus.Interview,
            application.AppliedDate,
            CancellationToken.None
        );

        Assert.NotNull(result);
        Assert.Equal(AddApplicationStatus.Success, result.Status);

        repo.Verify(
            e => e.AddApplication(
                application.CompanyName,
                application.Position,
                DatabaseApplicationStatus.Interview,
                application.AppliedDate,
                It.IsAny<CancellationToken>()
            )
        );
    }
}
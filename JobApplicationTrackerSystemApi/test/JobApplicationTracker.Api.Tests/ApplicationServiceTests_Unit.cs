using JobApplicationTracker.Api.Models.Shared;
using JobApplicationTracker.Api.Repositories;
using JobApplicationTracker.Api.Services;
using Moq;
using DatabaseApplicationStatus = JobApplicationTracker.Domain.Models.Enums.ApplicationStatus;
using DatabaseApplication = JobApplicationTracker.Domain.Models.Application;

namespace JobApplicationTracker.Api.Tests;

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
        var repo = new Mock<IApplicationRepository>();
        var service = new ApplicationService(repo.Object);

        await service.UpdateApplication(givenStatus, CancellationToken.None);

        repo.Verify(e => e.UpdateApplication(expectedStatus, It.IsAny<CancellationToken>()), Times.Once);
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

        var repo = new Mock<IApplicationRepository>();
        repo
            .Setup(e => e.GetApplication(application.Id, It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(application));
        var service = new ApplicationService(repo.Object);

        var result = await service.GetApplication(application.Id, CancellationToken.None);

        Assert.Equal(application.Id, result.Id);
        Assert.Equal(application.CompanyName, result.CompanyName);
        Assert.Equal(application.AppliedDate, result.AppliedDate);
        Assert.Equal(application.Position, result.Position);
        Assert.Equal(ApplicationStatus.Interview, result.Status);
    }

    [Fact]
    public async Task GetApplications_WithPaging_ReturnsCorrectSize()
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
            .Setup(e => e.GetPaginatedApplications(pageSize, currentPage, It.IsAny<CancellationToken>()))
            .ReturnsAsync(applications);

        var service = new ApplicationService(repo.Object);

        var result = await service.GetApplications(pageSize, currentPage, CancellationToken.None);
        Assert.Equal(pageSize, result.Count());
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
        var service = new ApplicationService(repo.Object);

        await service.AddApplication(
            application.CompanyName,
            application.Position,
            ApplicationStatus.Interview,
            application.AppliedDate,
            CancellationToken.None
        );

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
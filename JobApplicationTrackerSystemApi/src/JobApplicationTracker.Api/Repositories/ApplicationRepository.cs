using JobApplicationTracker.Domain;
using JobApplicationTracker.Domain.Models;
using JobApplicationTracker.Domain.Models.Enums;

namespace JobApplicationTracker.Api.Repositories;

public interface IApplicationRepository
{
    Task<Application> GetApplication(long id, CancellationToken token = default);

    Task<IEnumerable<Application>> GetPaginatedApplications(
        uint? pageSize,
        uint? pageNumber,
        CancellationToken token = default
    );

    Task AddApplication(
        string companyName,
        string position,
        ApplicationStatus status,
        DateTime dateApplied,
        CancellationToken token = default
    );

    Task UpdateApplication(long id, ApplicationStatus applicationStatus, CancellationToken token = default);
}

public class ApplicationRepository(JobApplicationTrackerDbContext dbContext) : IApplicationRepository
{
    public bool ApplicationExists(long id)
    {
        var application = dbContext.Applications.Find(id);
        return application != null;
    }

    public Task<Application> GetApplication(long id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Application>> GetPaginatedApplications(uint? pageSize, uint? pageNumber, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task AddApplication(
        string companyName,
        string position,
        ApplicationStatus status,
        DateTime dateApplied,
        CancellationToken token = default
    )
    {
        throw new NotImplementedException();
    }

    public Task UpdateApplication(ApplicationStatus applicationStatus, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateApplication(long id, ApplicationStatus applicationStatus, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
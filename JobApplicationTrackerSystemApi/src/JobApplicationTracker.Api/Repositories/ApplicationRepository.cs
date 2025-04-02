using JobApplicationTracker.Domain.Models;
using JobApplicationTracker.Domain.Models.Enums;

namespace JobApplicationTracker.Api.Repositories;

public interface IApplicationRepository
{
    bool ApplicationExists(long id);

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

    Task UpdateApplication(ApplicationStatus applicationStatus, CancellationToken token = default);
}

public class ApplicationRepository : IApplicationRepository
{
    public bool ApplicationExists(long id)
    {
        throw new NotImplementedException();
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

    public Task UpdateApplication(ApplicationStatus applicationStatus)
    {
        throw new NotImplementedException();
    }
}
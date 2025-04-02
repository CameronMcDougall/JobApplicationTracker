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

    Task UpdateApplication(ApplicationStatus applicationStatus);
}

public class ApplicationRepository : IApplicationRepository
{
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

    public Task UpdateApplication(ApplicationStatus applicationStatus)
    {
        throw new NotImplementedException();
    }
}
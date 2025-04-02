using JobApplicationTracker.Api.Models;
using JobApplicationTracker.Api.Models.Shared;
using JobApplicationTracker.Api.Repositories;

namespace JobApplicationTracker.Api.Services;

public interface IApplicationService
{
    Task<Application> GetApplication(long id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Application>> GetApplications(uint? pageSize, uint pageNumber, CancellationToken cancellationToken= default);

    Task AddApplication(string companyName, string position, ApplicationStatus applicationStatus, DateTime dateApplied);

    Task UpdateApplication(ApplicationStatus applicationStatus);
}

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _repository;

    public ApplicationService(IApplicationRepository repository)
    {
        _repository = repository;
    }

    public Task<Application> GetApplication(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Application>> GetApplications(uint? pageSize, uint pageNumber, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task AddApplication(string companyName, string position, ApplicationStatus applicationStatus, DateTime dateApplied)
    {
        throw new NotImplementedException();
    }

    public Task UpdateApplication(ApplicationStatus applicationStatus)
    {
        throw new NotImplementedException();
    }
}
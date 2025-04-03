using JobApplicationTracker.Api.Models;
using JobApplicationTracker.Api.Models.Results;
using JobApplicationTracker.Api.Models.Shared;
using JobApplicationTracker.Api.Repositories;

namespace JobApplicationTracker.Api.Services;

public interface IApplicationService
{
    Task<GetApplicationResult> GetApplication(long id, CancellationToken cancellationToken = default);

    Task<GetApplicationsResult> GetApplications(uint? pageSize, uint pageNumber, CancellationToken cancellationToken= default);

    Task<AddApplicationResult> AddApplication(string companyName, string position, ApplicationStatus applicationStatus, DateTime dateApplied, CancellationToken cancellationToken = default);

    Task<UpdateApplicationResult> UpdateApplication(ApplicationStatus applicationStatus, CancellationToken cancellationToken = default);
}

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _repository;

    public ApplicationService(IApplicationRepository repository)
    {
        _repository = repository;
    }

    public Task<GetApplicationResult> GetApplication(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<GetApplicationsResult> GetApplications(uint? pageSize, uint pageNumber, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AddApplicationResult> AddApplication(string companyName, string position, ApplicationStatus applicationStatus, DateTime dateApplied, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateApplicationResult> UpdateApplication(ApplicationStatus applicationStatus, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
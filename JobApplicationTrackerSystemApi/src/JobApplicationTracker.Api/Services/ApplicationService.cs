using AutoMapper;
using JobApplicationTracker.Api.Exceptions;
using JobApplicationTracker.Api.Models;
using JobApplicationTracker.Api.Models.Results;
using JobApplicationTracker.Api.Models.Results.Enums;
using JobApplicationTracker.Api.Models.Shared;
using JobApplicationTracker.Api.Repositories;

namespace JobApplicationTracker.Api.Services;

public interface IApplicationService
{
    Task<GetApplicationResult> GetApplication(long id, CancellationToken cancellationToken = default);

    GetApplicationsResult GetApplications(uint? pageSize, uint? pageNumber, PagingOrder order);

    Task<AddApplicationResult> AddApplication(
        string companyName,
        string position,
        ApplicationStatus applicationStatus,
        DateTime dateApplied,
        CancellationToken cancellationToken = default
    );

    Task<UpdateApplicationResult> UpdateApplication(
        long id,
        ApplicationStatus applicationStatus,
        CancellationToken cancellationToken = default
    );
}

public class ApplicationService(IApplicationRepository repository, IMapper mapper, ILogger<ApplicationService> logger)
    : IApplicationService
{
    public async Task<GetApplicationResult> GetApplication(long id, CancellationToken cancellationToken = default)
    {
        var application = await repository.GetApplication(id, cancellationToken);
        if (application == null)
        {
            logger.LogWarning("Application {Id} does not exist", id);
            return new GetApplicationResult
            {
                Status = GetApplicationStatus.ApplicationDoesNotExist
            };
        }

        var mappedApplication = mapper.Map<Application>(application);
        return new GetApplicationResult
        {
            Status = GetApplicationStatus.Success,
            Application = mappedApplication
        };
    }

    public GetApplicationsResult GetApplications(uint? pageSize, uint? pageNumber, PagingOrder order)
    {
        var paginatedApplications = repository.GetPaginatedApplications(pageSize, pageNumber, order);
        var mappedApplications = mapper.Map<IEnumerable<Application>>(paginatedApplications.Items);

        return new GetApplicationsResult
        {
            Applications = mappedApplications,
            Status = GetApplicationsStatus.Success,
            PagingInfo = paginatedApplications.PagingInfo
        };
    }

    public async Task<AddApplicationResult> AddApplication(
        string companyName,
        string position,
        ApplicationStatus applicationStatus,
        DateTime dateApplied,
        CancellationToken cancellationToken = default
    )
    {
        var mappedStatus = mapper.Map<Domain.Models.Enums.ApplicationStatus>(applicationStatus);
        await repository.AddApplication(companyName, position, mappedStatus, dateApplied, cancellationToken);

        return new AddApplicationResult
        {
            Status = AddApplicationStatus.Success
        };
    }

    public async Task<UpdateApplicationResult> UpdateApplication(
        long id,
        ApplicationStatus applicationStatus,
        CancellationToken cancellationToken = default
    )
    {
        var mappedStatus = mapper.Map<Domain.Models.Enums.ApplicationStatus>(applicationStatus);
        try
        {
            await repository.UpdateApplication(id, mappedStatus, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            logger.LogError(ex, "Failed to find application {Id}", id);
            return new UpdateApplicationResult
            {
                Status = UpdateApplicationStatus.ApplicationDoesNotExist
            };
        }

        return new UpdateApplicationResult
        {
            Status = UpdateApplicationStatus.Success
        };
    }
}
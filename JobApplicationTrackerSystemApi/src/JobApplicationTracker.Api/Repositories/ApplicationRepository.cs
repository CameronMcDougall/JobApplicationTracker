using JobApplicationTracker.Api.Exceptions;
using JobApplicationTracker.Api.Models;
using JobApplicationTracker.Domain;
using JobApplicationTracker.Domain.Models.Enums;
using Application = JobApplicationTracker.Domain.Models.Application;

namespace JobApplicationTracker.Api.Repositories;

public interface IApplicationRepository
{
    Task<Application?> GetApplication(long id, CancellationToken cancellationToken = default);

    PaginatedResult<Application> GetPaginatedApplications(
        uint? pageSize,
        uint? pageNumber,
        PagingOrder order
    );

    Task AddApplication(
        string companyName,
        string position,
        ApplicationStatus status,
        DateTime dateApplied,
        CancellationToken cancellationToken = default
    );

    Task UpdateApplication(long id, ApplicationStatus applicationStatus, CancellationToken cancellationToken = default);
}

public class ApplicationRepository(JobApplicationTrackerDbContext dbContext, ILogger<ApplicationRepository> logger)
    : IApplicationRepository
{
    public async Task<Application?> GetApplication(long id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Applications.FindAsync(id, cancellationToken);
    }

    public PaginatedResult<Application> GetPaginatedApplications(
        uint? pageSize,
        uint? pageNumber,
        PagingOrder order
    )
    {
        if (!pageSize.HasValue || !pageNumber.HasValue)
        {
            logger.LogDebug("PageSize and Page Number not provided. Skipping Pagination");
            return new PaginatedResult<Application>
            {
                Items = dbContext.Applications.AsEnumerable()
            };
        }

        var skippedAmount = pageSize.Value * pageNumber.Value;
        var applications = OrderApplications(order);
        var skipped = applications.Skip((int)skippedAmount).Take((int)pageSize.Value);

        var count = dbContext.Applications.Count();
        return new PaginatedResult<Application>
        {
            Items = skipped,
            PagingInfo = new PagingInfo
            {
                Current = (int)pageNumber,
                TotalItems = count,
                TotalPages = (int)Math.Ceiling((double)count / pageSize ?? 1)
            }
        };
    }

    public async Task AddApplication(
        string companyName,
        string position,
        ApplicationStatus status,
        DateTime dateApplied,
        CancellationToken cancellationToken = default
    )
    {
        var application = new Application(companyName, position, status, dateApplied);
        await dbContext.Applications.AddAsync(application, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateApplication(
        long id,
        ApplicationStatus applicationStatus,
        CancellationToken cancellationToken = default
    )
    {
        var application = await GetApplication(id, cancellationToken);
        if (application == null)
        {
            throw new EntityNotFoundException(nameof(Application));
        }

        application.Status = applicationStatus;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private IEnumerable<Application> OrderApplications(PagingOrder order)
    {
        return order switch
        {
            PagingOrder.Ascending => dbContext.Applications.OrderByDescending(e => e.Id),
            PagingOrder.Descending => dbContext.Applications.OrderBy(e => e.Id),
            _ => throw new ArgumentOutOfRangeException(nameof(order), order, null)
        };
    }
}
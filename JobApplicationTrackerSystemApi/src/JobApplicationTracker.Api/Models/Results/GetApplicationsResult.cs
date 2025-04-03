using JobApplicationTracker.Api.Models.Results.Enums;

namespace JobApplicationTracker.Api.Models.Results;

public class GetApplicationsResult
{
    public GetApplicationsStatus Status { get; set; }

    public IEnumerable<Application> Applications { get; set; }

    public PagingInfo? PagingInfo { get; set; }
}
using JobApplicationTracker.Api.Models.Results.Enums;

namespace JobApplicationTracker.Api.Models.Results;

public class GetApplicationResult
{
    public GetApplicationStatus Status { get; set; }

    public Application Application { get; set; }
}
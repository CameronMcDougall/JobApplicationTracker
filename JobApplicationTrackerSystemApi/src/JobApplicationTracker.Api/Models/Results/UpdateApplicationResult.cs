using JobApplicationTracker.Api.Models.Results.Enums;

namespace JobApplicationTracker.Api.Models.Results;

public class UpdateApplicationResult
{
    public UpdateApplicationStatus Status { get; set; }
}
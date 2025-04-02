using JobApplicationTracker.Api.Models.Shared;

namespace JobApplicationTracker.Api.Models;

public class Application
{
    public long Id { get; set; }

    public string CompanyName { get; set; }

    public string Position { get; set; }

    public ApplicationStatus Status { get; set; }

    public DateTime AppliedDate { get; set; }
}
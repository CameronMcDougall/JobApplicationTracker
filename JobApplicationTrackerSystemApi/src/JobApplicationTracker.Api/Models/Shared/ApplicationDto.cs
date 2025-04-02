namespace JobApplicationTracker.Api.Models.Shared;

public class ApplicationDto
{
    public long Id { get; set; }

    public string CompanyName { get; set; }

    public string Position { get; set; }

    public ApplicationStatusDto Status { get; set; }

    public DateTime AppliedDate { get; set; }
}
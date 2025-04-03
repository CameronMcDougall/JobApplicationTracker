using JobApplicationTracker.Api.Models.Shared;

namespace JobApplicationTracker.Api.Models.Requests;

public class AddApplicationRequestDto
{
    public string CompanyName { get; set; }
    
    public string Position { get; set; }

    public ApplicationStatusDto Status { get; set; }

    public DateTime DateApplied { get; set; }
}
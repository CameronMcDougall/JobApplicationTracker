using JobApplicationTracker.Api.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Models.Requests;

public class AddApplicationRequestDto
{
    [FromBody]
    public string CompanyName { get; set; }
    
    [FromBody]
    public string Position { get; set; }

    [FromBody]
    public ApplicationStatusDto Status { get; set; }

    [FromBody]
    public DateTime DateApplied { get; set; }
}
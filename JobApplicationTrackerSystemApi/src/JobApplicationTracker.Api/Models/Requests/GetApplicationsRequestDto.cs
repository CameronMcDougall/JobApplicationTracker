using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Models.Requests;

public class GetApplicationsRequestDto
{
    [FromQuery]
    public uint? PageSize { get; set; }

    [FromQuery]
    public uint? PageNumber { get; set; }
}
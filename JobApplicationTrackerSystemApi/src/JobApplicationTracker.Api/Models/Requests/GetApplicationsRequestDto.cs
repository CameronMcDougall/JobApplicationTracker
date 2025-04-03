using JobApplicationTracker.Api.Models.Requests.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Models.Requests;

public class GetApplicationsRequestDto
{
    [FromQuery]
    public uint? PageSize { get; set; }

    [FromQuery]
    public uint? PageNumber { get; set; }

    [FromQuery]
    public PagingOrderDto PageOrder { get; set; }
}
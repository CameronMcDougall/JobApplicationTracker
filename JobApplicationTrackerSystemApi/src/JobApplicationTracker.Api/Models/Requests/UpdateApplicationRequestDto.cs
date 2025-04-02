using JobApplicationTracker.Api.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Models.Requests;

public class UpdateApplicationRequestDto
{
    [FromBody]
    public ApplicationStatusDto Status { get; set; }
}
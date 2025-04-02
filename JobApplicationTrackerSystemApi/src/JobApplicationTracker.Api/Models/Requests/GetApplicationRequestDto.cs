using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Models.Requests;

public class GetApplicationRequestDto
{
    [FromRoute(Name = "id")]
    public long Id { get; set; }
}
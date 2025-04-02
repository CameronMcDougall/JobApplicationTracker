using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Models.Requests
{
    public class GetApplicationRequestDto
    {
        [FromQuery]
        public long Id { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Api.Models.Requests
{
    public class GetApplicationsRequest
    {
        [FromQuery]
        public uint? PageSize { get; set; }

        [FromQuery]
        public uint? PageNumber { get; set; }
    }
}

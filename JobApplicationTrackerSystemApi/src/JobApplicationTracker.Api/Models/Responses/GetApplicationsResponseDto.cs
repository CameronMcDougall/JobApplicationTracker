using JobApplicationTracker.Api.Models.Shared;

namespace JobApplicationTracker.Api.Models.Responses;

public class GetApplicationsResponseDto
{
    public IEnumerable<ApplicationDto> Applications { get; set; }

    public PagingInfoDto PagingInfo { get; set; }
}
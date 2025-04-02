namespace JobApplicationTracker.Api.Models.Shared;

public class PagingInfoDto
{
    public required int Current { get; set; }

    public required int TotalPages { get; set; }

    public required int TotalItems { get; set; }
}
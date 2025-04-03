namespace JobApplicationTracker.Api.Models;

public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; }

    public PagingInfo PagingInfo { get; set; }
}
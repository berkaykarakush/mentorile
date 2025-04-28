using Mentorile.Shared.Common;

public class PagedResult<T> : Result<List<T>>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public PagedResult() : base(200, "Success", new List<T>(), null)
    {
    }

    public PagedResult(List<T> data, int totalCount, PagingParams pagingParams, int statusCode, string message)
        : base(statusCode, message, data, null)
    {
        TotalCount = totalCount;
        CurrentPage = pagingParams.PageNumber;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pagingParams.PageSize);
        PageSize = pagingParams.PageSize;
    }

    public static PagedResult<T> Create(List<T> data, int totalCount, PagingParams pagingParams, int statusCode = 200, string message = "Success")
        => new PagedResult<T>(data, totalCount, pagingParams, statusCode, message);
}

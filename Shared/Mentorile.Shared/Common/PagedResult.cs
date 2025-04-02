namespace Mentorile.Shared.Common;
public class PagedResult<T> : Result<List<T>>
{
    // Gecerli sayfa numarasi
    public int CurrentPage { get; }

    // toplam sayfa sayisi
    public int TotalPages { get; }

    // sayfa basina oge sayisi
    public int PageSize { get; }

    // toplam oge sayisi
    public int TotalCount { get; }

    public PagedResult(List<T> data, int totalCount, PagingParams pagingParams, int statusCode, string message ): base(statusCode, message, data, null)
    {
        TotalCount = totalCount;
        CurrentPage = pagingParams.PageNumber;
        TotalPages = (int)Math.Ceiling(totalCount/(double)pagingParams.PageSize);
        PageSize = pagingParams.PageSize;
    }

    public static PagedResult<T> Create(List<T> data, int totalCount, PagingParams pagingParams, int statusCode = 200, string message = "Success")
        => new PagedResult<T>(data, totalCount, pagingParams, statusCode, message); 
}
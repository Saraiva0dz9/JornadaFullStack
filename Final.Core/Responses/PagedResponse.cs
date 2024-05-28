
using System.Text.Json.Serialization;

namespace Final.Core.Responses;

public class PagedResponse<TData> : Response<TData>
{
    [JsonConstructor]
    public PagedResponse(TData? data, int totalCount, int currentPage = 1, int pageSize = Configurations.DefaultPageSize): base(data)
    {
        this.TotalCount = totalCount;
        this.CurrentPage = currentPage;
        this.PageSize = pageSize;
    }

    public PagedResponse(TData? data, int code = Configurations.DefaultStatusCode, string? message = null) : base(data, code, message)
    {
    }

    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int PageSize { get; set; } = Configurations.DefaultPageSize;
    public int TotalCount { get; set; }
}

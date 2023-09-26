using System.Reflection;

namespace Valkyrie.Models;

public class QueryModel
{
    const int maxPageSize = 50;
    public string? SortBy { get; set; }
    public string SortDirection { get; set; } = "asc";
    public string? FilterByPlat { get; set; }
    public int CurrentPage { get; set; } = 1;
    private int pageSize = 10;

    public int PageSize 
    { 
        get => pageSize; 
        set => pageSize = (value > maxPageSize ? maxPageSize : value < 1 ? 10 : value);
    }

    public static ValueTask<QueryModel?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        const string filterByPlatKey = "filter";
        const string sortByKey = "sortBy";
        const string sortDirectionKey = "sortDir";
        const string currentPageKey = "page";
        const string pageSizeKey = "pageSize";

        int.TryParse(context.Request.Query[currentPageKey], out var page);
        int.TryParse(context.Request.Query[pageSizeKey], out var pageSize);

        page = page == 0 ? 1 : page;

        var result = new QueryModel
        {
            FilterByPlat = context.Request.Query[filterByPlatKey],
            SortBy = context.Request.Query[sortByKey],
            SortDirection = context.Request.Query[sortDirectionKey]!,
            CurrentPage = page,
            PageSize = pageSize
        };

        return ValueTask.FromResult<QueryModel?>(result);
    }
}

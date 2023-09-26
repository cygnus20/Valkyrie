using System.Globalization;
using System.Reflection;
using Valkyrie.DTOs;
using Valkyrie.Entities;
using Valkyrie.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Valkyrie.Extensions;

public static class Utilities
{
    public static DevboardDTO AsDTO(this DevBoard devboard) => new()
    {
        Guid = devboard.Guid,
        Name = devboard.Name,
        Description = devboard.Description,
        Platform = devboard.Platform,
        Family = devboard.Family,
        MainMCU = devboard.MainMCU,
        Pins = devboard.Pins,
        Communications = devboard.Communications,
        Power = devboard.Power,
        Dimensions = devboard.Dimensions,
        JTAG = devboard.JTAG

    };

    public static SBCDTO AsDTO(this SBC sbc) => new()
    {
        Guid = sbc.Guid,
        Name = sbc.Name,
        Description = sbc.Description,
        Platform = sbc.Platform,
        OperatingSystems = sbc.OperatingSystems,
        Sensors = sbc.Sensors,
        ExtraInterfaces = sbc.ExtraInterfaces,
        Power = sbc.Power,
        Pins = sbc.Pins,
        Communications = sbc.Communications,
        IO = sbc.IO,
        NetworkingAndComm = sbc.NetworkingAndComm,
        SpecialFeatures = sbc.SpecialFeatures
    };

    public static PageDevboardsModel PageBoards(HttpContext context, QueryModel query, IEnumerable<DevboardDTO> devboardDTOs, int count)
    {
        string qId = string.IsNullOrEmpty(context.Request.QueryString.ToString()) ? "" : "?";
        int page = query.CurrentPage;
        string pageSize = $"&pageSize={query.PageSize}";
        string sortBy = query.SortBy != null ? $"&sortBy={query.SortBy}" : "";
        string sortDir = !string.IsNullOrWhiteSpace(query.SortDirection) ? $"&sortDir={query.SortDirection}" : "";
        string filter = query.FilterByPlat != null ? $"&filter={query.FilterByPlat}" : "";
        string currentUrl = 
            $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}?page={page}{pageSize}{sortBy}{sortDir}{filter}";
        string? prevUrl = query.CurrentPage > 1 ?
            $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}?page={page - 1}{pageSize}{sortBy}{sortDir}{filter}" : null;
        string? nextUrl = query.CurrentPage < Math.Ceiling((double)(count / query.PageSize)) ?
            $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}?page={page + 1}{pageSize}{sortBy}{sortDir}{filter}" : null;

        PageLinks pageLinks = new(currentUrl, nextUrl, prevUrl);
        PageInfo pageInfo = new(query.CurrentPage, count, pageLinks);
        PageDevboardsModel devboardsModel = new(pageInfo, devboardDTOs);

        return devboardsModel;
    }

    public static PageSBCsModel PageBoards(HttpContext context, QueryModel query, IEnumerable<SBCDTO> sbcDTOs, int count)
    {
        string qId = string.IsNullOrEmpty(context.Request.QueryString.ToString()) ? "" : "?";
        int page = query.CurrentPage;
        string pageSize = $"&pageSize={query.PageSize}";
        string sortBy = query.SortBy != null ? $"&sortBy={query.SortBy}" : "";
        string sortDir = !string.IsNullOrWhiteSpace(query.SortDirection) ? $"&sortDir={query.SortDirection}" : "";
        string filter = query.FilterByPlat != null ? $"&filter={query.FilterByPlat}" : "";
        string currentUrl =
            $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}?page={page}{pageSize}{sortBy}{sortDir}{filter}";
        string? prevUrl = query.CurrentPage > 1 ?
            $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}?page={page - 1}{pageSize}{sortBy}{sortDir}{filter}" : null;
        string? nextUrl = query.CurrentPage < Math.Ceiling((double)(count / query.PageSize)) ?
            $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}?page={page + 1}{pageSize}{sortBy}{sortDir}{filter}" : null;

        PageLinks pageLinks = new(currentUrl, nextUrl, prevUrl);
        PageInfo pageInfo = new(query.CurrentPage, count, pageLinks);
        PageSBCsModel sbcsModel = new(pageInfo, sbcDTOs);

        return sbcsModel;
    }

    public static List<T> GetBoards<T>
        (IQueryable<T> dbItems, 
        List<T>? pageItems, 
        QueryModel query, 
        SortDir sortDir, 
        SortBy sortBy) where T : Board
    {
        int count = dbItems.Count();
        int skipNum = query.PageSize * (query.CurrentPage - 1);

        if (sortBy == SortBy.NAME)
        {
            if (sortDir == SortDir.DESC)
            {
                pageItems = pageItems?.OrderByDescending(i => i.Name).ToList() ??
                    dbItems.OrderByDescending(i => i.Name).Skip(skipNum).Take(query.PageSize).ToList();
            }

            else if (sortDir == SortDir.ASC)
            {
                pageItems = pageItems?.OrderBy(i => i.Name).ToList() ??
                    dbItems.OrderBy(i => i.Name).Skip(skipNum).Take(query.PageSize).ToList();
            }
        }

        else if (sortBy == SortBy.PLATFORM)
        {
            if (sortDir == SortDir.DESC)
            {
                pageItems = pageItems?.OrderByDescending(i => i.Platform).ToList() ??
                    dbItems.OrderByDescending(i => i.Platform).Skip(skipNum).Take(query.PageSize).ToList();
            }

            else if (sortDir == SortDir.ASC)
            {
                pageItems = pageItems?.OrderBy(i => i.Platform).ToList() ??
                    dbItems.OrderBy(i => i.Platform).Skip(skipNum).Take(query.PageSize).ToList();
            }
        }

        return pageItems ?? Enumerable.Empty<T>().ToList();
    }
}

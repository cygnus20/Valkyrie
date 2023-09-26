using Valkyrie.DTOs;

namespace Valkyrie.Models;

public record PageDevboardsModel(
    PageInfo PageInfo,
    IEnumerable<DevboardDTO> Devboards);
public record PageSBCsModel(
    PageInfo PageInfo,
    IEnumerable<SBCDTO> SBCS);
public record PageInfo(
    int PageNumber,
    int Count,
    PageLinks Links);
public record PageLinks(
    string CurrentPage,
    string? NextPage,
    string? PrevPage);

public enum SortDir
{
    ASC, DESC
}

public enum SortBy
{
    NAME, PLATFORM
}

namespace Valkyrie.Entities;

public record JTAG
{
    public string TDI { get; init; } = string.Empty;
    public string TDO { get; init; } = string.Empty;
    public string TCK { get; init; } = string.Empty;
    public string TMS { get; init; } = string.Empty;
    public string? TRST { get; init; }
}

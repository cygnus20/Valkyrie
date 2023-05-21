namespace Valkyrie.Entities;

public record JTAG
{
    public string TDI { get; init; } = "";
    public string TDO { get; init; } = "";
    public string TCK { get; init; } = "";
    public string TMS { get; init; } = "";
    public string? TRST { get; init; }
}

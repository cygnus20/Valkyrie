namespace Valkyrie.Entities;

public record NetworkComm
{
    public string Type { get; init; } = string.Empty;
    public string Details { get; init; } = string.Empty;
}

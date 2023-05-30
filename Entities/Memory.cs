namespace Valkyrie.Entities;

public record Memory
{
    public ulong Size { get; init; }
    public string Type { get; init; } = string.Empty;
}

namespace Valkyrie.Entities;

public abstract record Board
{
    public Guid Guid { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Platform { get; init; } = string.Empty;
}

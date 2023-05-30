using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

public record MCU
{
    public string Name { get; init; } = string.Empty;
    public string Architecture { get; init; } = string.Empty;
    public string Family { get; init; } = string.Empty;
    public ulong Frequency { get; init; }
    public List<Memory> Memories { get; init; } = Enumerable.Empty<Memory>().ToList();
}

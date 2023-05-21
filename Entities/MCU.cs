using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

public record MCU
{
    public string Name { get; init; } = "";
    public string Architecture { get; init; } = "";
    public string Family { get; init; } = "";
    public int Frequency { get; init; }
    public Dictionary<string, int> Memories { get; init; } = new();
}

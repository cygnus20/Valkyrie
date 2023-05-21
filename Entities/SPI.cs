using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

public record SPI
{
    public string SS { get; init; } = "";
    public string COPI { get; init; } = "";
    public string CIPO { get; init; } = "";
    public string SCK { get; init; } = "";
}

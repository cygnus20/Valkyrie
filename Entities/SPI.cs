using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

public record SPI
{
    public string CE { get; init; } = string.Empty;
    public string COPI { get; init; } = string.Empty;
    public string CIPO { get; init; } = string.Empty;
    public string SCK { get; init; } = string.Empty;
}

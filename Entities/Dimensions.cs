using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

public record Dimensions
{
    public float Weight { get; init; }
    public float Width { get; init; }
    public float Length { get; init; }
}

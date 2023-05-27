using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Valkyrie.Entities;

public record SBC
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public Guid Guid { get; init; } = Guid.Empty;
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public string Platform { get; init; } = "";
    [Column(TypeName = "jsonb")]
    public Power Power { get; init; } = new();
    [Column(TypeName = "jsonb")]
    public List<Pins> Pins { get; init; } = new();
}

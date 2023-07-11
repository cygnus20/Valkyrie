using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Valkyrie.Entities;

public record SBC
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public Guid Guid { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Platform { get; init; } = string.Empty;
    [Column(TypeName = "jsonb")]
    public List<string> OperatingSystems { get; init; } = Enumerable.Empty<string>().ToList();
    [Column(TypeName = "jsonb")]
    public List<string> Sensors { get; init; } = Enumerable.Empty<String>().ToList();
    [Column(TypeName = "jsonb")]
    public List<string>? ExtraInterfaces { get; init; }
    [Column(TypeName = "jsonb")]
    public Power Power { get; init; } = new();
    [Column(TypeName = "jsonb")]
    public Pins Pins { get; init; } = new();
    [Column(TypeName = "jsonb")]
    public Communications Communications { get; set; } = new();
    [Column(TypeName = "jsonb")]
    public List<string> IO { get; init; } = Enumerable.Empty<string>().ToList();

    [Column(TypeName = "jsonb")]
    public List<NetworkComm> NetworkingAndComm { get; set; } = new();
    [Column(TypeName = "jsonb")]
    public List<string> SpecialFeatures { get; init; } = Enumerable.Empty<string>().ToList();
}

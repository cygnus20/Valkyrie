using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

public record DevBoard
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public Guid Guid { get; init; } = Guid.Empty;
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public string Platform { get; init; } = "";
    public string Family { get; init; } = "";
    [Column(TypeName = "jsonb")]
    public MCU MainMCU { get; init; } = new MCU();
    [Column(TypeName = "jsonb")]
    public Pins Pins { get; init; } = new Pins();
    [Column(TypeName = "jsonb")]
    public Communications Communications { get; init; } = new();
    [Column(TypeName = "jsonb")]
    public Power Power { get; init; } = new();
    [Column(TypeName = "jsonb")]
    public Dimensions Dimensions { get; init; } = new();
    [Column(TypeName = "jsonb")]
    public JTAG? JTAG { get; init; }

}

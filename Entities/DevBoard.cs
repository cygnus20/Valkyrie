using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

public record DevBoard : Board
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public string Family { get; init; } = string.Empty;
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

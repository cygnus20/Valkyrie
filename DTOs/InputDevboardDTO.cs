using Valkyrie.Entities;

namespace Valkyrie.DTOs;

public record InputDevboardDTO
{
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public string Platform { get; init; } = "";
    public string Family { get; init; } = "";
    public MCU MainMCU { get; init; } = null!;
    public Pins Pins { get; init; } = null!;
    public Communications Communications { get; init; } = null!;
    public Power Power { get; init; } = null!;
    public Dimensions Dimensions { get; init; } = null!;
    public JTAG JTAG { get; init; } = null!;
}

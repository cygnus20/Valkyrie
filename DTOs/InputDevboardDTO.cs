using Valkyrie.Entities;

namespace Valkyrie.DTOs;

public record InputDevboardDTO
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Platform { get; init; } = string.Empty;
    public string Family { get; init; } = string.Empty;
    public MCU MainMCU { get; init; } = null!;
    public Pins Pins { get; init; } = null!;
    public Communications Communications { get; init; } = null!;
    public Power Power { get; init; } = null!;
    public Dimensions Dimensions { get; init; } = null!;
    public JTAG JTAG { get; init; } = null!;
}

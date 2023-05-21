using Valkyrie.Entities;

namespace Valkyrie.DTOs;

public record DevboardDTO
{
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Platform { get; init; }
    public string? Family { get; init; }
    public MCU? MainMCU { get; init; }
    public Pins? Pins { get; init; }
    public Communications? Communications { get; init; }
    public Power? Power { get; init; }
    public Dimensions? Dimensions { get; init; }
    public JTAG? JTAG { get; init; }
}

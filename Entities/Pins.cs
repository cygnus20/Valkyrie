using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

public record Pins
{
    public int DigitalIO { get; init; }
    public int AnalogInput { get; init; }
    public int PWM { get; init; }
    public List<string>? PWMPins { get; init; }
    public List<string>? BuiltinLEDPins { get; init; }
}

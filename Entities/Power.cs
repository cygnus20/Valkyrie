using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

public record Power
{
    public float IOVoltage { get; init; }
    public InputVoltage InputVoltage { get; init; } = new InputVoltage();
    public float IOPinCurrent { get; init; }
    public string PowerSupplyConnector { get; init; } = string.Empty;


}

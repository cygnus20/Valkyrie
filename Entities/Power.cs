using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

public record Power
{
    public int IOVoltage { get; init; }
    public InputVoltage InputVoltage { get; init; } = new InputVoltage();
    public int IOPinCurrent { get; init; }
    public string PowerSupplyConnector { get; init; } = "";


}

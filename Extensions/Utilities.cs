using Valkyrie.DTOs;
using Valkyrie.Entities;

namespace Valkyrie.Extensions;

public static class Utilities
{
    public static DevboardDTO AsDTO(this DevBoard devboard) => new DevboardDTO
    {
        Guid = devboard.Guid,
        Name = devboard.Name,
        Description = devboard.Description,
        Platform = devboard.Platform,
        Family = devboard.Family,
        MainMCU = devboard.MainMCU,
        Pins = devboard.Pins,
        Communications = devboard.Communications,
        Power = devboard.Power,
        Dimensions = devboard.Dimensions,
        JTAG = devboard.JTAG

    };

    public static SBCDTO AsDTO(this SBC sbc) => new SBCDTO
    {
        Guid = sbc.Guid,
        Name = sbc.Name,
        Description = sbc.Description,
        Platform = sbc.Platform,
        OperatingSystems = sbc.OperatingSystems,
        Sensors = sbc.Sensors,
        ExtraInterfaces = sbc.ExtraInterfaces,
        Power = sbc.Power,
        Pins = sbc.Pins,
        Communications = sbc.Communications,
        IO = sbc.IO,
        NetworkingAndComm = sbc.NetworkingAndComm,
        SpecialFeatures = sbc.SpecialFeatures
    };
}

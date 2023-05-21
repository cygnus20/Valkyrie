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
}

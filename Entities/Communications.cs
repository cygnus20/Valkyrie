using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

public record Communications
{
    public List<UART>? UART { get; init; }
    public List<I2C> I2C { get; init; } = Enumerable.Empty<I2C>().ToList();
    public List<SPI> SPI { get; init; } = Enumerable.Empty<SPI>().ToList();

}
 